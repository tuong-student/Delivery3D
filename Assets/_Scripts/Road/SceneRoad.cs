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
        [Inject] private GameManager _gameManager;

        void Start()
        {
            _gameManager.SetSceneRoad(this);
        }

        public Road ChooseRoad(RoadDirection roadDirection)
        {
            return _roadDic[roadDirection];
        }

        public RoadDirection[] GetRoadDirections()
        {
            return _roadDic.Keys.ToArray();
        }

    }

}
