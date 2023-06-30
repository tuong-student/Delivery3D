using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ImpossibleOdds.StateMachines;

namespace Game
{
    public enum PlayerState
    {
        Standing,
        Resting,
        WellWeight,
        MediumWeight,
        HeavyWeight,
        OverWeight
    }

    public class Player : MonoBehaviour
    {
        public PlayerState _playerState;
        [SerializeField] private bool editWeight;

        #region Weight
        [BoxGroup("Weight"), ShowIf(nameof(editWeight), true)]
        [SerializeField] private float _maxWeight;

        [BoxGroup("Weight"), ShowIf(nameof(editWeight), true)]
        [SerializeField] private float _currentWeight;

        [ShowInInspector]
        [BoxGroup("Weight"), ShowIf(nameof(editWeight), true)]
        [PropertyRange(0, nameof(_maxWeight))]
        [OnValueChanged(nameof(UpdateMediumWeight))]
        private float _lightWeight{
            get; set;
        }

        [ShowInInspector]
        [BoxGroup("Weight"), ShowIf("editWeight", true)]
        [PropertyRange(0, nameof(_maxWeight))]
        [OnValueChanged(nameof(UpdateHeavyWeight))]
        private float _mediumWeight{
            get; set;
        }

        [ShowInInspector]
        [BoxGroup("Weight"), ShowIf("editWeight", true)]
        [PropertyRange(0, nameof(_maxWeight))]
        private float _heavyWeight{
            get; set;
        }
        #endregion

        private void UpdateWellWeight()
        {

        }
        private void UpdateMediumWeight()
        {
            if(_lightWeight > _mediumWeight) _mediumWeight = _lightWeight;
        }

        private void UpdateHeavyWeight()
        {
            if(_mediumWeight > _heavyWeight) _heavyWeight = _mediumWeight;
        }
    }
}
