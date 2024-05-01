using DG.Tweening;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Scripts.States
{
    public class MovementState : State
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly string NextState = "Movement";
        
        [SerializeField] private Transform _whoMoved;
        [SerializeField] private Transform _oritentation;
        [SerializeField] private float _rotationDuration = 0.1f;
        [SerializeField] private float _acceleration;

        private Vector3 _moveDirection;
        private Animator _animator;
        private static readonly int IsMovement = Animator.StringToHash("IsMovement");

        private void OnEnable()
        {
            AnimationHandler animationHandler = _entity.Get<AnimationHandler>();
            _animator ??= animationHandler.Animator;
            _animator.SetBool(IsMovement, true);
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
            _animator.SetBool(IsMovement, false);
            _animator.SetFloat(Speed, 0);
        }
    }
}