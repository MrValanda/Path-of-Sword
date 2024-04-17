using DG.Tweening;
using Source.Scripts.Tools;
using Tools;
using UnityEngine;

namespace States
{
    public class MovementState : State
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationHandler _animationHandler;
        [SerializeField] private Transform _whoMoved;
        [SerializeField] private Transform _oritentation;
        [SerializeField] private float _rotationDuration = 0.1f;
        [SerializeField] private float _acceleration;

        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");
        private Vector3 _moveDirection;

        private void OnEnable()
        {
            _animationHandler.CrossFade("Movement",0, 0.1f);
        }

        protected override void OnUpdate()
        {
            Vector3 oritentationForward = _oritentation.forward;
            Vector3 oritentationRight = _oritentation.right;
            oritentationForward.y = 0;
            oritentationRight.y = 0;
            _moveDirection = oritentationForward * Input.GetAxisRaw("Vertical") +
                             oritentationRight * Input.GetAxisRaw("Horizontal");

            _moveDirection.Normalize();

            _animator.SetFloat(Speed,
                Mathf.Lerp(_animator.GetFloat(Speed), _moveDirection.magnitude, Time.deltaTime * _acceleration));


            if (_moveDirection != Vector3.zero)
            {
                _whoMoved.DOKill();
                _whoMoved.DORotateQuaternion(Quaternion.LookRotation(_moveDirection), _rotationDuration);
            }
        }

        protected override void OnExit()
        {
            _animator.SetFloat(Speed, 0);
        }
    }
}