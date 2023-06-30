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
        [SerializeField] private string RUNNING_STATE, JOGGING_STATE, WALKING_STATE;
        [SerializeField] private Animator _animator;

        public void SetAnim(AnimStage animStage)
        {
            switch(animStage)
            {
                case AnimStage.Idle:
                    _animator.SetBool(RUNNING_STATE, false);
                    _animator.SetBool(JOGGING_STATE, false);
                    _animator.SetBool(WALKING_STATE, false);
                    break;
                case AnimStage.Running:
                    _animator.SetBool(RUNNING_STATE, true);
                    break;
                case AnimStage.Jogging:
                    _animator.SetBool(JOGGING_STATE, true);
                    break;
                case AnimStage.Walking:
                    _animator.SetBool(WALKING_STATE, true);
                    break;
            }
        }
    }
}
