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
