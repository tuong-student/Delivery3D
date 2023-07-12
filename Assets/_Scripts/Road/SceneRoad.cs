using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using NOOD;
using ImpossibleOdds.DependencyInjection;
using ImpossibleOdds;

namespace Game
{
    [System.Serializable]
    public enum RoadDirection
    {
        Beginning,
        Left,
        Forward,
        Right,
    }

    [ShowOdinSerializedPropertiesInInspector]
    [Injectable]
    [ExecuteAfter(typeof(GameManager))]
    public class SceneRoad : MonoBehaviour
    {
        [SerializeField] private CustomDictionary<RoadDirection, Road> _roadDic = new CustomDictionary<RoadDirection, Road>();
        private GameManager _gameManager;
        private Town _previousTown;

        void Awake()
        {
        }
        void Start()
        {
            UIEvent.onSetDirectionBtnRequest.Invoke(this);
        }

        public void SetGameManager(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        public void SetPreviousTown(Town previousTown)
        {
            int i = 0;
            if(previousTown == null)
            {
                // No previousTown - this is at the beginning of the game
                foreach(var key in _roadDic.Keys)
                {
                    // Set the start town to the portal
                    _roadDic[key].SetNextTown(TownNetworkManager.GetTownNetwork()[0]);
                    i++;
                }
                return;
            }


            this._previousTown = previousTown;
            List<Town> nextTowns = previousTown.GetAllConnectedTown();
            foreach(var key in _roadDic.Keys)
            {
                _roadDic[key].SetNextTown(nextTowns[i]);
                i++;
            }
        }

        public Road ChooseRoad(RoadDirection roadDirection)
        {
            return _roadDic[roadDirection];
        }

        public RoadDirection[] GetRoadDirections()
        {
            return _roadDic.Keys.ToArray();
        }
        public Vector3 GetBeginningPoint()
        {
            return _roadDic[RoadDirection.Beginning].GetPoint(0);
        }

    }

}
