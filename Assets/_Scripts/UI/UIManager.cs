using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ImpossibleOdds.DependencyInjection;

namespace Game
{
    public enum ButtonType
    {
        Left,
        Forward,
        Right
    }

    [Injectable]
    public class UIManager : MonoBehaviour, IDependencyScopeInstaller
    {
        public Action<RoadDirection> onUIButtonDirectionPress;

        public void Install(IDependencyContainer container)
        {
            container.Register<UIManager>(new InstanceBinding<UIManager>(this));
        }

        [SerializeField] private Button _leftBtn, _rightBtn, _forwardBtn;
        [Inject] private GameManager _gameManager;

        void Awake()
        {
            _leftBtn.onClick.AddListener(() => onUIButtonDirectionPress?.Invoke(RoadDirection.Left));
            _forwardBtn.onClick.AddListener(() => onUIButtonDirectionPress?.Invoke(RoadDirection.Forward));
            _rightBtn.onClick.AddListener(() => onUIButtonDirectionPress?.Invoke(RoadDirection.Right));
            SceneRoad.onSceneLoad += SetBtn;
        }

        void Start()
        {
        }

        private void SetBtn(SceneRoad sceneRoad)
        {
            HideBtn();
            foreach(var roadDirection in sceneRoad.GetRoadDirections())
            {
                if(roadDirection == RoadDirection.Left)
                {
                    _leftBtn.gameObject.SetActive(true);
                }
                if(roadDirection == RoadDirection.Right)
                {
                    _rightBtn.gameObject.SetActive(true);
                }
                if(roadDirection == RoadDirection.Forward)
                {
                    _forwardBtn.gameObject.SetActive(true);
                }
            }
        }

        private void ActiveBtn(bool active)
        {
            _leftBtn.gameObject.SetActive(active);
            _rightBtn.gameObject.SetActive(active);
            _forwardBtn.gameObject.SetActive(active);
        }

        private void ShowBtn()
        {
            ActiveBtn(true);
        }
        private void HideBtn()
        {
            ActiveBtn(false);
        }
    }

}
