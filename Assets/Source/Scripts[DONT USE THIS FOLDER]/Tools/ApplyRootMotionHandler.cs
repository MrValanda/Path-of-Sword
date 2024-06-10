using Source.Modules.MovementModule.Scripts;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Scripts.Tools
{
    public class ApplyRootMotionHandler : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _animationRootMotionMultiplayer = 1;
        private float _verticalSpeed;

        public void SetAnimationRootMotionMultiplier(float newMultiplier)
        {
            _animationRootMotionMultiplayer = newMultiplier;
        }

        public float GetAnimationMotionMultiplier()
        {
            return _animationRootMotionMultiplayer;
        }

        private void OnAnimatorMove()
        {
            if (_entity.Contains<MoveToTargetComponent>()) return;

            Vector3 animatorMove = _animator.deltaPosition * _animationRootMotionMultiplayer;
            Vector3 moveWithGravity = animatorMove + Vector3.up * _verticalSpeed;
            moveWithGravity.x = 0;
            moveWithGravity.z = 0;
            if (Mathf.Abs(animatorMove.y) == 0)
            {
                _verticalSpeed += Physics.gravity.y * Time.deltaTime;
                _characterController.Move(moveWithGravity * Time.deltaTime);
            }
            else
            {
                _verticalSpeed = 0;
            }


            if (_characterController.isGrounded)
            {
                _verticalSpeed = 0f;
            }
          
            _characterController.Move(animatorMove);
        }
    }
}