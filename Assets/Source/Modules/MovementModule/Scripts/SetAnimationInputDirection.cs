using Source.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class SetAnimationInputDirection : State
    {
        private static readonly int InputY = Animator.StringToHash("InputY");
        private static readonly int InputX = Animator.StringToHash("InputX");
        private MovementStateData _movementStateData;
        private Vector3 _moveDirection;
        private Animator _animator;

        private void OnEnable()
        {
            AnimationHandler animationHandler = _entity.Get<AnimationHandler>();
            _movementStateData ??= _entity.Get<MovementStateData>();
            _animator ??= animationHandler.Animator;
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

            Vector3 inputDirection = _movementStateData.WhoMoved.InverseTransformDirection(_moveDirection);

            _animator.SetFloat(InputX, inputDirection.x, 0.1f, Time.deltaTime);
            _animator.SetFloat(InputY, inputDirection.z, 0.1f, Time.deltaTime);
        }
    }
}