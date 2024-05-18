using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UniRx;
using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class DodgeState : State
    {
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
            _animationHandler.Animator.SetLayerWeight(_animationHandler.Animator.GetLayerIndex(DodgeLayerName), 1);
            _animationHandler.Animator.SetTrigger(Dodge);
            _disposable?.Dispose();
        }

        private void OnDisable()
        {
            float weight = 1;
            _disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                weight = Mathf.Lerp(weight, 0, Time.deltaTime * 2);
                _animationHandler.Animator.SetLayerWeight(_animationHandler.Animator.GetLayerIndex("DodgeLayer"),
                    weight);
            });
        }

        private void FixedUpdate()
        {
            if (_direction == Vector3.zero) return;

            _dodgeStateData.WhoWasRotate.forward = _direction;
        }
    }
}