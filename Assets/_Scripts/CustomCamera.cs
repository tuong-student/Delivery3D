using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using ImpossibleOdds.DependencyInjection;

namespace Game
{
    [Injectable]
    public class CustomCamera : MonoBehaviour, IDependencyScopeInstaller
    {
        private NOOD.NoodCamera.CameraFollow _cameraFollow;
        [SerializeField] private CinemachineVirtualCamera _cameraVirtual;
        [Inject] private Player _player;
        [SerializeField] private float _trackOffsetY = 6.24f;
        [SerializeField] private float _returnDuration = 2f;
        private float _elapsedTime = 0;
        private bool _isReturning = false;

        void Awake()
        {
            _cameraVirtual = GetComponent<CinemachineVirtualCamera>();
            _cameraFollow = GetComponent<NOOD.NoodCamera.CameraFollow>();
        }
        void Update()
        {
            if(_isReturning && _elapsedTime < _returnDuration)
            {
                _elapsedTime += Time.deltaTime;
                _cameraVirtual.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.y = Mathf.Lerp(0, _trackOffsetY, _elapsedTime/_returnDuration);
            }
            else _isReturning = false;
        }

        private void SetTrackedObjectOffsetY(float value)
        {
            _cameraVirtual.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.y = value;
        }
        public void SetCameraTarget(Transform target)
        {
            if(!target.CompareTag("Player"))
                SetTrackedObjectOffsetY(0);
            else
                SetTrackedObjectOffsetY(_trackOffsetY);

            _cameraVirtual.LookAt = target;
            _isReturning = false;
        }

        public void TranslateCamera(Vector3 newPosition)
        {
            _cameraFollow.isFollow = false;
            TranslateCamera(newPosition, null);
        }
        public void TranslateCamera(Vector3 newPosition, System.Action callback = null)
        {
            _cameraFollow.isFollow = false;
            this.transform.DOMove(newPosition, 1f).SetEase(Ease.Flash).OnComplete(() => callback?.Invoke());
        }

        public void ReturnOldPosition()
        {
            this.transform.DOKill();
            _cameraFollow.isFollow = true;
            _cameraVirtual.LookAt = _player.transform;
            _elapsedTime = 0;
            _isReturning = true;
        }

        public void Install(IDependencyContainer container)
        {
            container.Register<CustomCamera>(new InstanceBinding<CustomCamera>(this));
        }
    }
}
