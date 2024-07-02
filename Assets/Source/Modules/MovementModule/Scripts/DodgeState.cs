using System;
using Cinemachine.Utility;
using Source.CodeLibrary.ServiceBootstrap;
using Source.Modules.AudioModule;
using Source.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
public class DodgeTag {}

    public class DodgeState : State
    {
        private static readonly string DodgeLayerName = "DodgeLayer";
        private static readonly int Dodge = Animator.StringToHash("Dodge");
        private static readonly int IsDodge = Animator.StringToHash("IsDodge");

        [SerializeField] private float _dodgeForce;
        [SerializeField] private float _dodgeStopForce;

        private Vector3 _direction;
        private AnimationHandler _animationHandler;
        private DodgeStateData _dodgeStateData;
        private IDisposable _disposable;

        protected override void OnEnter()
        {
            _dodgeStateData = _entity.Get<DodgeStateData>();
            ServiceLocator.For(this).Get<SoundPlayer>().PlaySoundByType(SoundType.Dodge);
             Vector3 orientationForward = _dodgeStateData.Orientation.forward;
            Vector3 orientationRight = _dodgeStateData.Orientation.right;
             orientationForward.y = 0;
             orientationRight.y = 0;
             Vector3 moveDirection = orientationForward * Input.GetAxisRaw("Vertical") +
                                    orientationRight * Input.GetAxisRaw("Horizontal");
             if (moveDirection == Vector3.zero)
             {
                 moveDirection = -_dodgeStateData.WhoWasRotate.forward;
             }
            //
            // _direction = moveDirection;
            // if (_direction != Vector3.zero)
            // {
            //     _dodgeStateData.WhoWasRotate.forward = moveDirection;
            // }

            _animationHandler ??= _entity.Get<AnimationHandler>();
            AddForceDirectionComponent addForceDirectionComponent = _entity.AddOrGet<AddForceDirectionComponent>();
            addForceDirectionComponent.WhoWillMoveEntity = _entity;
            addForceDirectionComponent.Execute(moveDirection.normalized * _dodgeForce,
                _dodgeStopForce);
            _animationHandler.Animator.SetTrigger(Dodge);
            _animationHandler.Animator.SetBool(IsDodge, true);
            _animationHandler.Animator.SetLayerWeight(_animationHandler.Animator.GetLayerIndex("DodgeLayer"),
                1);
            _entity.Add(new DodgeTag());
        }

        protected override void OnExit()
        {
            _entity.Remove<DodgeTag>();
            _entity.Remove<AddForceDirectionComponent>();
            _animationHandler.Animator.SetBool(IsDodge, false);
            _animationHandler.Animator.SetLayerWeight(_animationHandler.Animator.GetLayerIndex("DodgeLayer"),
                1);
        }

        private void FixedUpdate()
        {
            if (_direction == Vector3.zero) return;

            _dodgeStateData.WhoWasRotate.forward = _direction;
        }
    }
}