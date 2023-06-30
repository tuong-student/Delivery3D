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
        [GlobalScopeInstaller]
        public void Install(IDependencyContainer container)
        {
            container.Register<Player>(new InstanceBinding<Player>(this));
        }
        
        public PlayerState _playerState;
        [SerializeField] private bool editWeight;

        #region Weight
        [BoxGroup("Weight"), ShowIf(nameof(editWeight), true)]
        [SerializeField] private float _maxWeight;

        [BoxGroup("Weight"), ShowIf(nameof(editWeight), true)]
        [SerializeField] private float _currentWeight;

        [ShowInInspector]
        [BoxGroup("Weight"), ShowIf("editWeight", true)]
        [PropertyRange(0, 1)]
        [LabelText("MediumWeight%")]
        [OnValueChanged(nameof(UpdateHeavyWeight))]
        private float _mediumWeightRation;

        [ShowInInspector]
        [BoxGroup("Weight"), ShowIf("editWeight", true)]
        [PropertyRange(0, 1)]
        [LabelText("HeavyWeight%")]
        [OnValueChanged(nameof(UpdateMediumWeight))]
        private float _heavyWeightRation;

        private float _mediumWeight, _heavyWeigh;

        #endregion
        private void UpdateMediumWeight()
        {
            if(_heavyWeightRation < _mediumWeightRation) _mediumWeightRation = _heavyWeightRation;
        }

        private void UpdateHeavyWeight()
        {
            if(_mediumWeightRation > _heavyWeightRation) _heavyWeightRation = _mediumWeightRation;
        }

        public void SetMaxWeight(float amount)
        {
            this._maxWeight = amount;
            this._mediumWeight = this._maxWeight * _mediumWeightRation;
            this._heavyWeigh = this._maxWeight * _heavyWeightRation;
        }
        private void AddMaxWeight(float amount)
        {
            SetMaxWeight(this._maxWeight + amount);
        }

        private void AddCurrentWeight(float amount)
        {
            this._currentWeight += amount;
            if(_currentWeight < _mediumWeight)
            {
                this._playerState = PlayerState.LightWeight;
            }
            if(_currentWeight >= _mediumWeight)
            {
                this._playerState = PlayerState.MediumWeight;
            }
            if(_currentWeight >= _heavyWeigh)
            {
                this._playerState = PlayerState.HeavyWeight;
            }
            if(_currentWeight >= _maxWeight)
            {
                this._playerState = PlayerState.OverWeight;
            }
        }
        private void MinusCurrentWeight(float amount)
        {
            this._currentWeight -= amount;
        }

    }
}
