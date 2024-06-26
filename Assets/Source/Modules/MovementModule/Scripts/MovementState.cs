using DG.Tweening;
using Source.Modules.Tools;
using Source.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class MovementState : State
    {
        private static readonly int InputY = Animator.StringToHash("InputY");
        private static readonly int InputX = Animator.StringToHash("InputX");
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
                Mathf.Lerp(_animator.GetFloat(Speed), _moveDirection.magnitude,
                    Time.deltaTime * _movementStateData.Acceleration));

            Vector3 inputDirection = _movementStateData.WhoMoved.InverseTransformDirection(_moveDirection);
            
            _animator.SetFloat(InputX, inputDirection.x,0.1f,Time.deltaTime);
            _animator.SetFloat(InputY, inputDirection.z,0.1f,Time.deltaTime);
            
            if (_moveDirection != Vector3.zero && _entity.Contains<DisableAnimatorMoveOneFrameComponent>() == false)
            {
                _movementStateData.WhoMoved.DOKill();
                _movementStateData.WhoMoved.DORotateQuaternion(Quaternion.LookRotation(_moveDirection),
                    _movementStateData.RotationDuration);
            }
        }

        protected override void OnExit()
        {
            _movementStateData.WhoMoved.DOKill();
            _animator.SetBool(IsMovement, false);
            _animator.SetFloat(Speed, 0);
        }
    }
}