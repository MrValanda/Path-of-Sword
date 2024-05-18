using DG.Tweening;
using Source.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class MovementState : State
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMovement = Animator.StringToHash("IsMovement");

        private MovementStateData _movementStateData;
        private Vector3 _moveDirection;
        private Animator _animator;

        private void OnEnable()
        {
            AnimationHandler animationHandler = _entity.Get<AnimationHandler>();
            _movementStateData ??= _entity.Get<MovementStateData>();
            _animator ??= animationHandler.Animator;
            _animator.SetBool(IsMovement, true);
        }

        protected override void OnUpdate()
        {
            Vector3 orientationForward = _movementStateData.Orientation.forward;
            Vector3 orientationRight = _movementStateData.Orientation.right;
            orientationForward.y = 0;
            orientationRight.y = 0;
            _moveDirection = orientationForward * Input.GetAxisRaw("Vertical") +
                             orientationRight * Input.GetAxisRaw("Horizontal");

            _moveDirection.Normalize();

            _animator.SetFloat(Speed,
                Mathf.Lerp(_animator.GetFloat(Speed), _moveDirection.magnitude, Time.deltaTime * _movementStateData.Acceleration));


            if (_moveDirection != Vector3.zero)
            {
                _movementStateData.WhoMoved.DOKill();
                _movementStateData.WhoMoved.DORotateQuaternion(Quaternion.LookRotation(_moveDirection),
                    _movementStateData.RotationDuration);
            }
        }

        protected override void OnExit()
        {
            _animator.SetBool(IsMovement, false);
            _animator.SetFloat(Speed, 0);
        }
    }
}