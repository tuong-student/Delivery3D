using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using NOOD;
using ImpossibleOdds.DependencyInjection;

namespace Game
{
    [System.Serializable]
    public enum RoadDirection
    {
        Left,
        Forward,
        Right,
    }

    [ShowOdinSerializedPropertiesInInspector]
    [Injectable]
    public class SceneRoad : MonoBehaviour, IDependencyScopeInstaller
    {
        public static Action<SceneRoad> onSceneLoad;
        [SerializeField] private CustomDictionary<RoadDirection, Road> _roadDic = new CustomDictionary<RoadDirection, Road>();
        [Inject] private GameManager _gameManager;

        private IDependencyContainer container;
        public void Install(IDependencyContainer container)
        {
            container.Register<SceneRoad>(new InstanceBinding<SceneRoad>(this));
            this.Inject(container);
        }

        void Start()
        {
            container = FindObjectOfType<AbstractDependencyScopeBehaviour>().DependencyContainer;
            Install(container);
            _gameManager.SetSceneRoad(this);
            UIEvent.onUISetBtnRequest.Invoke(this);
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
