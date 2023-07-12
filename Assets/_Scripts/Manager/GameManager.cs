using System.Collections.Generic; using UnityEngine;
using ImpossibleOdds.DependencyInjection;
using ImpossibleOdds;
using System;

namespace Game
{
    public enum GameState
    {
        OnTown,
        OnRoad,
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
        [Inject]
        private GameLoader _gameLoader;
        private SceneRoad _sceneRoad;
        private TownManager _townManager;
        private GameState _gameState;
        private Town _currentTown;

        void Awake()
        {

        }
        void Start()
        {
            if(_currentTown._name.IsNullOrEmpty())
            {
                _gameLoader.LoadToRoad(null);
            }
            TownNetworkManager.CreateTownNetwork(10);
            _gameState = GameState.OnRoad;
            UIEvent.onDirectionButtonPress.Register(ChooseRoad);
        }
        void OnDestroy()
        {
            UIEvent.PurgeDelegatesOf(this);
        }

        public void CreatePlayerIfNeed()
        {
            if(_player == null)
            {
                // Create a new player at the beginning point and set the beginning road to that player
                _player = Player.Create();
                _player.transform.position = _sceneRoad.GetBeginningPoint();
                _player.SetCanMove(true);
            }
        }

        public void SetSceneRoad(SceneRoad sceneRoad)
        {
            this._sceneRoad = sceneRoad;
            this._sceneRoad.SetGameManager(this);
            UIEvent.onSetDirectionBtnRequest.Invoke(_sceneRoad);
            CreatePlayerIfNeed();
            if(_player.IsHadRoad())
            {

            }
            else
            {
                _player.SetRoad(sceneRoad.ChooseRoad(RoadDirection.Beginning));
            }
        }
        public void SetTownManager(TownManager townManager)
        {
            this._townManager = townManager;
            CreatePlayerIfNeed();
        }
        public void SetCurrentTown(Town currentTown)
        {
            this._currentTown = currentTown;
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
            _player.SetCanMove(true);
        }

        public GameState GetGameState()
        {
            return _gameState;
        }
    }

}
