using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Stamina : MonoBehaviour
    {
        [SerializeField] private float _currentStamina;
        [SerializeField] private float _maxStamina;
        [SerializeField] private float _lightConsumption, _mediumConsumption, _heavyConsumption;

        void Start()
        {
            _currentStamina = _maxStamina;
        }

        public float CurrentStamina {
            get 
            { 
                return _currentStamina; 
            }
            set 
            {
                _currentStamina = value; 
                if(_currentStamina > _maxStamina)
                {
                    _currentStamina = _maxStamina;
                }
                if(_currentStamina < 0) _currentStamina = 0;
            }
        }
        public float MaxStamina{
            get
            {
                return _maxStamina;
            }
            set 
            {
                _maxStamina = value;
            }
        } 

        public void AddStamina(float amount)
        {
            this.CurrentStamina += amount;
        }

        public void MinusStamina(PlayerState playerState)
        {
            switch(playerState)
            {
                case PlayerState.LightWeight:
                    _currentStamina -= _lightConsumption;
                    break;
                case PlayerState.MediumWeight:
                    _currentStamina -= _lightConsumption;
                    break;
                case PlayerState.HeavyWeight:
                    _currentStamina -= _lightConsumption;
                    break;
            }
        }
    }
}