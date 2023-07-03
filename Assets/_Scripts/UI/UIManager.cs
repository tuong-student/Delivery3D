using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ImpossibleOdds.DependencyInjection;
using ImpossibleOdds;

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
        public void Install(IDependencyContainer container)
        {
            container.Register<UIManager>(new InstanceBinding<UIManager>(this));
        }

        [SerializeField] private Button _leftBtn, _rightBtn, _forwardBtn;
        [Inject] private GameManager _gameManager;
        [Inject] private Player _player;
        private bool _isLeft, _isRight, _isForward;

        void Awake()
        {
            _leftBtn.onClick.AddListener(() =>
            {
                UIEvent.onDirectionButtonPress.Invoke(RoadDirection.Left);
                HideBtn();
            });
            _rightBtn.onClick.AddListener(() => 
            {
                UIEvent.onDirectionButtonPress.Invoke(RoadDirection.Right);
                HideBtn();
            });
            _forwardBtn.onClick.AddListener(() => 
            {
                UIEvent.onDirectionButtonPress.Invoke(RoadDirection.Forward);
                HideBtn();
            });

            UIEvent.onUISetBtnRequest.Register(SetBtn);
            UIEvent.onUIButtonRequester.Register(ActiveBtn);
        }

        void Start()
        {
        }

        void OnDestroy()
        {
            UIEvent.PurgeDelegatesOf(this);
        }

        private void SetBtn(SceneRoad sceneRoad)
        {
            HideBtn();
            _isLeft = _isRight = _isForward = false;
            foreach(var roadDirection in sceneRoad.GetRoadDirections())
            {
                if(roadDirection == RoadDirection.Left)
                {
                    // _leftBtn.gameObject.SetActive(true);
                    _isLeft = true;
                }
                if(roadDirection == RoadDirection.Right)
                {
                    // _rightBtn.gameObject.SetActive(true);
                    _isRight = true;
                }
                if(roadDirection == RoadDirection.Forward)
                {
                    // _forwardBtn.gameObject.SetActive(true);
                    _isForward = true;
                }
            }
        }

        private void ActiveBtn(bool active)
        {
            if(active == false)
            {
                _leftBtn.gameObject.SetActive(false);
                _rightBtn.gameObject.SetActive(false);
                _forwardBtn.gameObject.SetActive(false);
            }
            else
            {
                _leftBtn.gameObject.SetActive(_isLeft);
                _rightBtn.gameObject.SetActive(_isRight);
                _forwardBtn.gameObject.SetActive(_isForward);
            }
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
