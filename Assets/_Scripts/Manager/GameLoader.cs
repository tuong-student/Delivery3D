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
        [SerializeField] private List<TownManager> _towns = new List<TownManager>();

        public void Install(IDependencyContainer container)
        {
            container.Register<GameLoader>(new InstanceBinding<GameLoader>(this));
        }

        public void LoadToRoad()
        {
            // Load the road with animation then give it to game manager
            TransitionManager.Instance().onTransitionCutPointReached += SetRoadToGameManager;
            TransitionManager.Instance().Transition(_loadToRoadSetting, _loadTime);
        }
        public void LoadToTown()
        {
            // Load the town/townManager with animation then give it to game manager
            TransitionManager.Instance().onTransitionCutPointReached += SetTownManagerToGameManager;
            TransitionManager.Instance().Transition(_loadToTownSetting, _loadTime);
        }

        private void SetRoadToGameManager()
        {
            SceneRoad sceneRoad = Instantiate(_maps[UnityEngine.Random.Range(0, _maps.Count)], Vector3.zero, Quaternion.identity).GetComponentInChildren<SceneRoad>();
            _gameManager.SetSceneRoad(sceneRoad);
        }
        private void SetTownManagerToGameManager()
        {
            TownManager townManager = Instantiate(_towns[UnityEngine.Random.Range(0, _towns.Count)], Vector3.zero, Quaternion.identity);
            _gameManager.SetTownManager(townManager);
        }
    }

}
