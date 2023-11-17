using UnityEngine;

public class ApplyRootMotionHandler : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    private float _verticalSpeed;
    [SerializeField] private float _animationRootMotionMultiplayer = 1;

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
        Vector3 animatorMove = _animator.deltaPosition * _animationRootMotionMultiplayer;


        Vector3 moveWithGravity = animatorMove + Vector3.up * _verticalSpeed;
        _characterController.Move(animatorMove);
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
    }
}