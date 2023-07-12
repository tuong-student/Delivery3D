using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum PortalType
    {
        TownPortal,
        RoadPortal
    }

    public class Portal : MonoBehaviour 
    {
        [SerializeField] private PortalType _type;
        private GameLoader _gameLoader;
        private Town _town;

        void Awake()
        {
            _gameLoader = DependenceInjectionSceneScope.ContainerInstance.GetInstance<GameLoader>();
        }

        void OnTriggerEnter(Collider other)
        {
            if(_type == PortalType.RoadPortal)
                _gameLoader.LoadToTown(_town);
            else
                _gameLoader.LoadToRoad(_town);
        }

        public void SetTown(Town town)
        {
            _town = town;
        }
    }

}
