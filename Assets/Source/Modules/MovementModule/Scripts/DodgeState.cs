using System;
using Source.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class DodgeState : State
    {
        [SerializeField] private float _dodgeForce;
        [SerializeField] private float _dodgeStopForce;
        private static readonly string DodgeLayerName = "DodgeLayer";
        private static readonly int Dodge = Animator.StringToHash("Dodge");

        private Vector3 _direction;
        private AnimationHandler _animationHandler;
        private DodgeStateData _dodgeStateData;
        private IDisposable _disposable;

        protected override void OnEnter()
        {
            _dodgeStateData = _entity.Get<DodgeStateData>();
            Vector3 orientationForward = _dodgeStateData.Orientation.forward;
            Vector3 orientationRight = _dodgeStateData.Orientation.right;
            orientationForward.y = 0;
            orientationRight.y = 0;
            Vector3 moveDirection = orientationForward * Input.GetAxisRaw("Vertical") +
                                    orientationRight * Input.GetAxisRaw("Horizontal");

            _direction = moveDirection;
            if (_direction != Vector3.zero)
            {
                _dodgeStateData.WhoWasRotate.forward = moveDirection;
            }

            _animationHandler ??= _entity.Get<AnimationHandler>();
            AddForceDirectionComponent addForceDirectionComponent = _entity.AddOrGet<AddForceDirectionComponent>();
            addForceDirectionComponent.WhoWillMoveEntity = _entity;
            addForceDirectionComponent.Execute(_dodgeStateData.WhoWasRotate.forward.normalized * _dodgeForce,
                _dodgeStopForce);
            _animationHandler.Animator.SetTrigger(Dodge);
            _animationHandler.Animator.SetLayerWeight(_animationHandler.Animator.GetLayerIndex("DodgeLayer"),
                1);
        }

        protected override void OnExit()
        {
            _entity.Remove<AddForceDirectionComponent>();
            _animationHandler.Animator.SetLayerWeight(_animationHandler.Animator.GetLayerIndex("DodgeLayer"),
                0);
        }

        private void FixedUpdate()
        {
            if (_direction == Vector3.zero) return;

            _dodgeStateData.WhoWasRotate.forward = _direction;
        }
    }
}