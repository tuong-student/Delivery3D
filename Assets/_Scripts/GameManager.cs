using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImpossibleOdds.DependencyInjection;
using System;

namespace Game
{
    public enum GameState
    {
        Playing,
        Pause
    }

    [Injectable]
    public class GameManager : MonoBehaviour, IDependencyScopeInstaller
    {
        public void Install(IDependencyContainer container)
        {
            container.Register<GameManager>(new InstanceBinding<GameManager>(this));
        }

        [Inject]
        private UIManager _UImanager;
        [Inject]
        private Player _player;
        private SceneRoad _sceneRoad;

        void Awake()
        {
            SceneRoad.onSceneLoad += SetSceneRoad;
        }

        void Start()
        {
            _UImanager.onUIButtonDirectionPress += ChooseRoad;
        }

        private void SetSceneRoad(SceneRoad sceneRoad)
        {
            this._sceneRoad = sceneRoad;
        }

        private void ChooseRoad(RoadDirection buttonType)
        {
            Road road = null;
            switch(buttonType)
            {
                case RoadDirection.Left:
                    road = _sceneRoad.ChooseRoad(RoadDirection.Left);
                    break;
                case RoadDirection.Right:
                    road = _sceneRoad.ChooseRoad(RoadDirection.Right);
                    break;
                case RoadDirection.Forward:
                    road = _sceneRoad.ChooseRoad(RoadDirection.Forward);
                    break;
            }
            _player.SetRoad(road);
        }
    }

}
