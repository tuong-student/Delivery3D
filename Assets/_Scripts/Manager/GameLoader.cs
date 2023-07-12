using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImpossibleOdds.DependencyInjection;
using EasyTransition;

namespace Game
{
    [Injectable]
    public class GameLoader : MonoBehaviour, IDependencyScopeInstaller
    {
        [Inject] private GameManager _gameManager;
        [SerializeField] private TransitionSettings _loadToRoadSetting, _loadToTownSetting;
        [SerializeField] private float _loadTime = 1f;
        [SerializeField] private List<GameObject> _maps = new List<GameObject>();
        [SerializeField] private List<GameObject> _map2s = new List<GameObject>();
        [SerializeField] private List<GameObject> _map3s = new List<GameObject>();
        [SerializeField] private List<TownManager> _towns = new List<TownManager>();
        private Town _targetTown;
        private Town _fromTown;

        public void Install(IDependencyContainer container)
        {
            container.Register<GameLoader>(new InstanceBinding<GameLoader>(this));
        }

        public void LoadToRoad(Town fromTown = null)
        {
            // Load the road with animation then give it to game manager
            Debug.Log("Load to town");
            _fromTown = fromTown;
            TransitionManager.Instance().onTransitionCutPointReached += SetRoadToGameManager;
            TransitionManager.Instance().Transition(_loadToRoadSetting, _loadTime);
        }
        public void LoadToTown(Town targetTown)
        {
            // Load the town/townManager with animation then give it to game manager
            _targetTown = targetTown;
            TransitionManager.Instance().onTransitionCutPointReached += SetTownManagerToGameManager;
            TransitionManager.Instance().Transition(_loadToTownSetting, _loadTime);
        }

        private void SetRoadToGameManager()
        {
            SceneRoad sceneRoad = null;
            if(_fromTown == null)
            {
                // At the beginning of the game
                sceneRoad = Instantiate(_maps[UnityEngine.Random.Range(0, _maps.Count)], Vector3.zero, Quaternion.identity).GetComponentInChildren<SceneRoad>();
                sceneRoad.SetPreviousTown(null);
                _gameManager.SetSceneRoad(sceneRoad);
                return;
            }

            switch(_fromTown.GetAllConnectedTown().Count)
            {
                case 1:
                    sceneRoad = Instantiate(_maps[UnityEngine.Random.Range(0, _maps.Count)], Vector3.zero, Quaternion.identity).GetComponentInChildren<SceneRoad>();
                    break;
                case 2:
                    sceneRoad = Instantiate(_map2s[UnityEngine.Random.Range(0, _maps.Count)], Vector3.zero, Quaternion.identity).GetComponentInChildren<SceneRoad>();
                    break;
                case 3:
                    sceneRoad = Instantiate(_map3s[UnityEngine.Random.Range(0, _maps.Count)], Vector3.zero, Quaternion.identity).GetComponentInChildren<SceneRoad>();
                    break;
            }
            sceneRoad.SetPreviousTown(_fromTown);
            _gameManager.SetSceneRoad(sceneRoad);
        }
        private void SetTownManagerToGameManager()
        {
            TownManager townManager = Instantiate(_towns[UnityEngine.Random.Range(0, _towns.Count)], Vector3.zero, Quaternion.identity);
            townManager.SetCurrentTown(_targetTown);
            _gameManager.SetTownManager(townManager);
        }
    }

}
