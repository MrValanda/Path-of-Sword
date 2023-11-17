using System.Collections.Generic;
using DG.Tweening;
using Tools;
using UnityEngine;
using XftWeapon;

namespace States
{
    public class AttackState : State
    {
        [SerializeField] private AttackEventListener attackEventListener;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private List<MoveSet> _moveSets;
        [SerializeField] private AnimationHandler _animator;
        [SerializeField] private Transform _orientation;
        [SerializeField] private Transform _whoWasRotate;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private XWeaponTrail _xWeaponTrailDemo;
        [SerializeField] private Weapon _weapon;

        private int _currentAttackIndex;
        private int _currentAttackAnimationIndex;
        private bool _isAttacking;
        private readonly List<string> _attacksAnimators = new List<string>() {"Attack", "Attack2"};

        protected override void OnEnter()
        {
            _xWeaponTrailDemo.Activate();
            _weapon.Enable();
            attackEventListener.AttackStarted += OnAttackStarted;
            attackEventListener.AttackEnded += OnAttackEnded;
            attackEventListener.AttackReset += OnAttackReset;
            Attack();
        }

        protected override void OnExit()
        {
            attackEventListener.AttackStarted -= OnAttackStarted;
            attackEventListener.AttackEnded -= OnAttackEnded;
            attackEventListener.AttackReset -= OnAttackReset;
            _weapon.Disable();
            _xWeaponTrailDemo.Deactivate();
            _currentAttackIndex = 0;
            _isAttacking = false;
        }

        protected override void OnUpdate()
        {
            if (_isAttacking == false && Input.GetMouseButtonDown(0))
            {
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

            _animator.OverrideAnimation(_attacksAnimators[_currentAttackAnimationIndex],
                _moveSets[_currentAttackIndex].AnimationClip);


            _animator.CrossFade(_attacksAnimators[_currentAttackAnimationIndex++], 0, 0.1f);

            _currentAttackAnimationIndex %= _attacksAnimators.Count;
        }

        private void OnAttackStarted()
        {
            _weapon.Enable();
            Debug.LogError("Start");

            if (_moveSets[_currentAttackIndex].SpawnedEffect != null)
            {
                // Instantiate(_moveSets[_currentAttackIndex].SpawnedEffect, _spawnPoint).transform.parent = null;
            }
        }

        private void OnAttackEnded()
        {
            _isAttacking = false;
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
}