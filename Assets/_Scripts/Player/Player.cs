using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ImpossibleOdds.DependencyInjection;
using DG.Tweening;

namespace Game
{
    public enum PlayerStatus
    {
        Standing,
        Resting,
        LightWeight,
        MediumWeight,
        HeavyWeight,
        OverWeight
    }

    [Injectable]
    public class Player : MonoBehaviour, IDependencyScopeInstaller
    {
        public void Install(IDependencyContainer container)
        {
            container.Register<Player>(new InstanceBinding<Player>(this));
        }

        public static Player Create(Transform parent = null)
        {
            return Instantiate<Player>(Resources.Load<Player>("Prefabs/Player"), parent);
        }

        public PlayerStatus _playerStatus;
        private bool _canMove;
        private bool _eventPlaceMove;
        private Road _currentRoad;
        private float _currentSpeed;
        private EventPlace _eventPlace;
        private List<Package> _packageList = new List<Package>();
        private float _money;
        private Vector3 _eventPlacePosition;
        [Inject] private CustomCamera _camera;

        [SerializeField] private Transform _cameraBesideTransform;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private Stamina _stamina;
        [SerializeField] private Vector3 _currentPointPosition;
        [SerializeField] private Rigidbody _myBody;
        [SerializeField] private float _lightWeightSpeed = 5f, _mediumWeightSpeed = 3f, _heavyWeightSpeed = 2f;

        #region Weight
        [BoxGroup("Weight")]
        [SerializeField] private float _maxWeight;

        [BoxGroup("Weight")]
        [SerializeField] private float _currentWeight;

        [ShowInInspector]
        [BoxGroup("Weight")]
        [Range(0, 1)]
        [LabelText("MediumWeight%")]
        private float _mediumWeightRation = 0.5f;

        [ShowInInspector]
        [BoxGroup("Weight")]
        [Range(0, 1)]
        [LabelText("HeavyWeight%")]
        private float _heavyWeightRation = 0.8f;
        #endregion

        #region UpdateProperty
        public void SetMaxWeight(float amount)
        {
            this._maxWeight = amount;
        }
        private void AddMaxWeight(float amount)
        {
            SetMaxWeight(this._maxWeight + amount);
        }

        private void AddCurrentWeight(float amount)
        {
            this._currentWeight += amount;
            CheckPlayerStatus();
        }
        private void MinusCurrentWeight(float amount)
        {
            this._currentWeight -= amount;
            CheckPlayerStatus();
        }
        private void CheckPlayerStatus()
        {
            if((_currentWeight/_maxWeight) < _mediumWeightRation)
            {
                this._playerStatus = PlayerStatus.LightWeight;
            }
            if((_currentWeight/_maxWeight) >= _mediumWeightRation)
            {
                this._playerStatus = PlayerStatus.MediumWeight;
            }
            if((_currentWeight/_maxWeight) >= _heavyWeightRation)
            {
                this._playerStatus = PlayerStatus.HeavyWeight;
            }
            if(_currentWeight >= _maxWeight)
            {
                this._playerStatus = PlayerStatus.OverWeight;
            }
        }
        #endregion

        #region UnityMethods
        void Awake()
        {
            _camera = DependenceInjectionSceneScope.ContainerInstance.GetInstance<CustomCamera>();
        }
        void Start()
        {
            _camera.SetCameraTarget(this.transform);
            CheckPlayerStatus();
        }
        void Update()
        {
            _playerView._playerStatus = _playerStatus;
            Move();
            switch(_playerStatus)
            {
                case PlayerStatus.Standing:
                case PlayerStatus.Resting:
                case PlayerStatus.OverWeight:
                    Stop();
                    break;
                case PlayerStatus.LightWeight:
                    _currentSpeed = _lightWeightSpeed;
                    break;
                case PlayerStatus.MediumWeight:
                    _currentSpeed = _mediumWeightSpeed;
                    break;
                case PlayerStatus.HeavyWeight:
                    _currentSpeed = _heavyWeightSpeed;
                    break;
            }
        }
        #endregion

        public void SetRoad(Road road) 
        {
            if(road == null) return;
            this._currentRoad = road;
            _currentPointPosition = _currentRoad.GetNextPointPosition();
        }
        public bool IsHadRoad()
        {
            return this._currentRoad != null;
        }

    #region MovementZone
        private void Move()
        {
            if(_eventPlaceMove)
                EventPlaceMove();
            if(_canMove == false || _currentRoad == null) return;
            NormalMove();
        }
        private void EventPlaceMove()
        {
            CheckPlayerStatus();
            _playerView.Move();
            if(MoveToPosition(_eventPlacePosition))
            {
                Stop();
                _eventPlaceMove = false;
                _eventPlace.OnPlayerGetToEventPoint();
            }
        }
        private void NormalMove()
        {
            _playerView.Move();
            if(MoveToPosition(_currentPointPosition))
            {
                if(CheckIfStopPoint())
                {
                    Stop();
                    UIEvent.onDirectionButtonOnOffRequester.Invoke(true);
                }
                else
                {
                    _currentPointPosition = _currentRoad.GetNextPointPosition();
                }
            }
        }
        /// <summary>
        /// Move player to position, return false if did get to position, return true if get to position
        /// </summary>
        /// <param name="position"> destination </param>
        /// <returns></returns>
        private bool MoveToPosition(Vector3 position)
        {
            Vector3 moveDirection = NOOD.NoodyCustomCode.LookDirectionSameHigh(this.transform.position, position);
            this.transform.forward = moveDirection;
            if(Vector3.Distance(this.transform.position, position) < 0.5f)
            {
                // Got to destination
                return true;
            }
            else
            {
                this.transform.position += moveDirection * _currentSpeed * Time.deltaTime;
                return false;
            }
        }
        public void Stop()
        {
            _playerStatus = PlayerStatus.Standing;
            _playerView.Stop();
            SetCanMove(false);
        }
    #endregion

        public void SetCanMove(bool canMove)
        {
            this._canMove = canMove;
            if(canMove)
                CheckPlayerStatus();
        }
        public void SetEventPlacePosition(Vector3 position)
        {
            this._eventPlacePosition = position;
            this._eventPlaceMove = true;
        }

        private bool CheckIfStopPoint()
        {
            if(_currentRoad.IsLastPoint())
            {
                return true;
            }
            else return false;
        }

        public void SetEventPlace(EventPlace place)
        {
            this._eventPlace = place;
        }

        public void AddPackage(Package package)
        {
            this._packageList.Add(package);
        }
        public void RemovePackage(Package package)
        {
            this._packageList.Remove(package);
        }
        public void AddMoney(float amount)
        {
            this._money += amount;
        }
        public bool TryToSpendMoney(float amount)
        {
            if(amount > this._money)
            {
                return false;
            }
            this._money -= amount;
            return true;
        }

        public Vector3 GetCameraBesidePosition()
        {
            return this._cameraBesideTransform.position;
        }
    }
}
