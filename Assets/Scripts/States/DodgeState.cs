using Tools;
using UnityEngine;

namespace States
{
    public class DodgeState : State
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationHandler _animationHandler;
        [SerializeField] private Transform _orientation;
        [SerializeField] private Transform _whoWasRotate;
    

        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");
        private static readonly int Dodge = Animator.StringToHash("Dodge");


        protected override void OnEnter()
        {
            
            Vector3 oritentationForward = _orientation.forward;
            Vector3 oritentationRight = _orientation.right;
            oritentationForward.y = 0;
            oritentationRight.y = 0;
            Vector3 moveDirection = oritentationForward * Input.GetAxisRaw("Vertical") +
                                    oritentationRight * Input.GetAxisRaw("Horizontal");

            if (moveDirection == Vector3.zero)
            {
                _animator.SetFloat(InputX, 0);
                _animator.SetFloat(InputY, 0);
            }
            else
            {
                _whoWasRotate.forward = moveDirection;
                _animator.SetFloat(InputX, 0);
                _animator.SetFloat(InputY, 1);
            }


            _animationHandler.CrossFade("Dodge", 0, 0.1f);
            // _animator.SetTrigger(Dodge);
        }
    }
}