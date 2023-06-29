using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImpossibleOdds.StateMachines;

namespace Game
{
    public enum AnimStage
    {
        Idle,
        Running,
        Jogging,
        Walking
    }

    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void SetAnim(AnimStage animStage)
        {
            switch(animStage)
            {
                case AnimStage.Idle:
                break;
            }
        }
    }
}
