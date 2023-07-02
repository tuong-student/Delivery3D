using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImpossibleOdds.StateMachines;

namespace Game
{
    public enum AnimState
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
        public PlayerState _playerState;

        public void Move()
        {
            switch(_playerState)
            {
                case PlayerState.LightWeight:
                    SetAnim(AnimState.Running);
                    break;
                case PlayerState.MediumWeight:
                    SetAnim(AnimState.Jogging);
                    break;
                case PlayerState.HeavyWeight:
                    SetAnim(AnimState.Walking);
                    break;
                case PlayerState.OverWeight:
                    SetAnim(AnimState.Idle);
                    break;
            }
        }

        public void Stop()
        {
            switch(_playerState)
            {
                case PlayerState.Standing:
                case PlayerState.Resting:
                    SetAnim(AnimState.Idle);
                    break;
            }
        }

        private void SetAnim(AnimState animStage)
        {
            switch(animStage)
            {
                case AnimState.Idle:
                    _animator.SetBool(RUNNING_STATE, false);
                    _animator.SetBool(JOGGING_STATE, false);
                    _animator.SetBool(WALKING_STATE, false);
                    break;
                case AnimState.Running:
                    _animator.SetBool(RUNNING_STATE, true);
                    break;
                case AnimState.Jogging:
                    _animator.SetBool(JOGGING_STATE, true);
                    break;
                case AnimState.Walking:
                    _animator.SetBool(WALKING_STATE, true);
                    break;
            }
        }
    }
}
