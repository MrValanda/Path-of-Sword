using System;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Scripts.States
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

        private Vector3 _direction;
        protected override void OnEnter()
        {
            Vector3 orientationForward = _orientation.forward;
            Vector3 orientationRight = _orientation.right;
            orientationForward.y = 0;
            orientationRight.y = 0;
            Vector3 moveDirection = orientationForward * Input.GetAxisRaw("Vertical") +
                                    orientationRight * Input.GetAxisRaw("Horizontal");

            _direction = moveDirection;
            _whoWasRotate.forward = moveDirection;
            _animationHandler.Animator.SetLayerWeight(_animationHandler.Animator.GetLayerIndex("DodgeLayer"), 1);
            _animationHandler.Animator.SetTrigger(Dodge);
            // _animator.SetTrigger(Dodge);
        }

        private void FixedUpdate()
        {
            _whoWasRotate.forward = _direction;
        }

        private void OnDisable()
        {
            _animationHandler.Animator.SetLayerWeight(_animationHandler.Animator.GetLayerIndex("DodgeLayer"), 0);
        }
    }
}