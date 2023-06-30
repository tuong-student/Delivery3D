using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Stamina : MonoBehaviour
    {
        [SerializeField] private float _currentStamina;
        [SerializeField] private float _maxStamina;

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
    }
}