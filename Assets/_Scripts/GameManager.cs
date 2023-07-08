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
        private Player _player;
        private SceneRoad _sceneRoad;

        void Awake()
        {
        }

        void Start()
        {
            UIEvent.onDirectionButtonPress.Register(ChooseRoad);
        }

        void OnDestroy()
        {
            UIEvent.PurgeDelegatesOf(this);
        }

        public void SetSceneRoad(SceneRoad sceneRoad)
        {
            this._sceneRoad = sceneRoad;
            UIEvent.onSetDirectionBtnRequest.Invoke(_sceneRoad);
            if(_player.IsHadRoad())
            {

            }
            else
            {
                _player.SetRoad(sceneRoad.ChooseRoad(RoadDirection.Beginning));
            }
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
