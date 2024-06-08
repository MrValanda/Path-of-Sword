using System;
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
            SmoothDodge(0, 1, 10);

            _animationHandler.Animator.SetTrigger(Dodge);
        }

        private void OnDisable()
        {
            SmoothDodge(1, 0, 2);
        }

        private void SmoothDodge(float baseWeight,float endWeight,float speed)
        {
            _disposable?.Dispose();
            int layerIndex = _animationHandler.Animator.GetLayerIndex("DodgeLayer");
            _disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                baseWeight = Mathf.Lerp(baseWeight, endWeight, Time.deltaTime * speed);
                _animationHandler.Animator.SetLayerWeight(layerIndex,
                    baseWeight);
            });
        }

        private void FixedUpdate()
        {
            if (_direction == Vector3.zero) return;

            _dodgeStateData.WhoWasRotate.forward = _direction;
        }
    }
}