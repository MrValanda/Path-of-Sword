using System;
using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;
using XftWeapon;

namespace Source.Scripts.States
{
    public class AttackState : State
    {
        [SerializeField] private List<MoveSet> _moveSets;
        [SerializeField] private Transform _orientation;
        [SerializeField] private Transform _whoWasRotate;
        [SerializeField] private float _rotationSpeed;

        private XWeaponTrail _xWeaponTrailDemo;
        private Weapon _weapon;
        private AnimationHandler _animationHandler;

        private int _currentAttackIndex;
        private int _currentAttackAnimationIndex;
        private bool _isAttacking;
        private readonly List<string> _attacksAnimators = new List<string>() {"Attack", "Attack2"};
        private AttackEventListener _attackEventListener;
        private static readonly int IsAttack = Animator.StringToHash("IsAttacking");

        protected override void OnEnter()
        {
            _animationHandler ??= _entity.Get<AnimationHandler>();
            _weapon ??= _entity.Get<Weapon>();
            _xWeaponTrailDemo ??= _entity.Get<XWeaponTrail>();
            _attackEventListener ??= _entity.Get<AttackEventListener>();

            _animationHandler.Animator.SetBool(IsAttack, true);

            _xWeaponTrailDemo.Activate();
            _attackEventListener.AttackStarted += OnAttackStarted;
            _attackEventListener.AttackEnded += OnAttackEnded;
            _attackEventListener.AttackReset += OnAttackReset;
            Attack();
        }

        protected override void OnExit()
        {
            _entity.Get<ApplyRootMotionHandler>().SetAnimationRootMotionMultiplier(1);
            _attackEventListener.AttackStarted -= OnAttackStarted;
            _attackEventListener.AttackEnded -= OnAttackEnded;
            _attackEventListener.AttackReset -= OnAttackReset;
            _weapon.Disable();
            _xWeaponTrailDemo.Deactivate();
            _currentAttackIndex = 0;
            _isAttacking = false;
            _animationHandler.Animator.SetBool(IsAttack, false);
        }

        protected override void OnUpdate()
        {
            if (_isAttacking == false && Input.GetMouseButtonDown(0))
            {
                _weapon.Disable();
                Attack();
            }
        }

        private void Attack()
        {
            _isAttacking = true;
            Vector3 oritentationForward = _orientation.forward;
            Vector3 oritentationRight = _orientation.right;
            oritentationForward.y = 0;
            oritentationRight.y = 0;
            Vector3 moveDirection = oritentationForward * Input.GetAxisRaw("Vertical") +
                                    oritentationRight * Input.GetAxisRaw("Horizontal");


            _whoWasRotate.DORotateQuaternion(
                moveDirection != Vector3.zero
                    ? Quaternion.LookRotation(moveDirection)
                    : Quaternion.LookRotation(oritentationForward), _rotationSpeed);

            _entity.Get<ApplyRootMotionHandler>()
                .SetAnimationRootMotionMultiplier(_moveSets[_currentAttackIndex].RootMultiplierBeforeEndAttack);

            _animationHandler.OverrideAnimation(_attacksAnimators[_currentAttackAnimationIndex],
                _moveSets[_currentAttackIndex].AnimationClip);

            _animationHandler.Animator.SetTrigger(_attacksAnimators[_currentAttackAnimationIndex++]);

            _currentAttackAnimationIndex %= _attacksAnimators.Count;
        }

        private void OnAttackStarted()
        {
            _weapon.Enable();
            Debug.LogError("Start");
        }

        private void OnAttackEnded()
        {
            _isAttacking = false;

            _entity.Get<ApplyRootMotionHandler>()
                .SetAnimationRootMotionMultiplier(_moveSets[_currentAttackIndex].RootMultiplierAfterEndAttack);

            _weapon.Disable();
            Debug.LogError("End");
            _currentAttackIndex++;
            _currentAttackIndex %= _moveSets.Count;
        }

        private void OnAttackReset()
        {
            Debug.LogError("Reset");

            _weapon.Disable();
            _currentAttackIndex = 0;
        }
    }

    [Serializable]
    public class MoveSet
    {
        [field: SerializeField] public AnimationClip AnimationClip { get; private set; }
        [field: SerializeField] public float RootMultiplierBeforeEndAttack { get; private set; }
        [field: SerializeField] public float RootMultiplierAfterEndAttack { get; private set; }
    }
}