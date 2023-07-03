using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ImpossibleOdds.DependencyInjection;

namespace Game
{
    public enum PlayerState
    {
        Standing,
        Resting,
        LightWeight,
        MediumWeight,
        HeavyWeight,
        OverWeight
    }

    public class Player : MonoBehaviour, IDependencyScopeInstaller
    {
        public void Install(IDependencyContainer container)
        {
            container.Register<Player>(new InstanceBinding<Player>(this));
        }

        public PlayerState _playerState;
        private bool _canMove;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private Stamina _stamina;
        [SerializeField] private Vector3 _currentPointPosition;
        private Road _currentRoad;
        [SerializeField] private Rigidbody _myBody;

        [SerializeField] private float _lightWeightSpeed = 5f, _mediumWeightSpeed = 3f, _heavyWeightSpeed = 2f;
        private float _currentSpeed;

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
            CheckCurrentWeight();
        }
        private void MinusCurrentWeight(float amount)
        {
            this._currentWeight -= amount;
            CheckCurrentWeight();
        }
        private void CheckCurrentWeight()
        {
            if((_currentWeight/_maxWeight) < _mediumWeightRation)
            {
                this._playerState = PlayerState.LightWeight;
            }
            if((_currentWeight/_maxWeight) >= _mediumWeightRation)
            {
                this._playerState = PlayerState.MediumWeight;
            }
            if((_currentWeight/_maxWeight) >= _heavyWeightRation)
            {
                this._playerState = PlayerState.HeavyWeight;
            }
            if(_currentWeight >= _maxWeight)
            {
                this._playerState = PlayerState.OverWeight;
            }
        }
        #endregion

        #region UnityMethods
        void Start()
        {
            CheckCurrentWeight();
        }

        void Update()
        {
            _playerView._playerState = _playerState;
            Move();
            switch(_playerState)
            {
                case PlayerState.Standing:
                case PlayerState.Resting:
                case PlayerState.OverWeight:
                    _playerView.Stop();
                    break;
                case PlayerState.LightWeight:
                    _currentSpeed = _lightWeightSpeed;
                    break;
                case PlayerState.MediumWeight:
                    _currentSpeed = _mediumWeightSpeed;
                    break;
                case PlayerState.HeavyWeight:
                    _currentSpeed = _heavyWeightSpeed;
                    break;
            }
        }
        #endregion

        public void SetRoad(Road road)
        {
            if(road == null) return;
            SetCanMove(true);
            this._currentRoad = road;
            _currentPointPosition = _currentRoad.GetNextPointPosition();
            Move();
        }
        public bool IsHadRoad()
        {
            return this._currentRoad != null;
        }

        private void Move()
        {
            if(_canMove == false || _currentRoad == null) return;
            _playerView.Move();
            Vector3 moveDirection = NOOD.NoodyCustomCode.LookDirectionSameHigh(this.transform.position, _currentPointPosition);
            this.transform.forward = moveDirection;
            if(Vector3.Distance(this.transform.position, _currentPointPosition) < 0.5f)
            {
                if(CheckIfStopPoint())
                {
                    Stop();
                    UIEvent.onUIButtonRequester.Invoke(true);
                }
                else
                    _currentPointPosition = _currentRoad.GetNextPointPosition();
            }
            else
            {
                this.transform.position += moveDirection * _currentSpeed * Time.deltaTime;
            }
        }

        public void Stop()
        {
            _playerState = PlayerState.Standing;
            _playerView.Stop();
            SetCanMove(false);
        }
        public void SetCanMove(bool canMove)
        {
            this._canMove = canMove;
            if(canMove)
                CheckCurrentWeight();
        }

        private bool CheckIfStopPoint()
        {
            if(_currentRoad.IsLastPoint())
            {
                return true;
            }
            else return false;
        }
    }
}
