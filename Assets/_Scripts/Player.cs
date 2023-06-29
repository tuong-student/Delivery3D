using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImpossibleOdds.StateMachines;

namespace Game
{
    public enum PlayerState
    {
        WellWeight,
        MediumWeight,
        OverWeight,
        HardOverWeight
    }

    public class Player : MonoBehaviour
    {
        [SerializeField] private StateMachine<PlayerState> _playerState = new StateMachine<PlayerState>();

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
