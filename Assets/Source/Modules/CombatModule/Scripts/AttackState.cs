using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.Utilities;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts;
using Source.Scripts.CombatModule;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using Source.Scripts.WeaponModule;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    public class AttackState : State
    {
        private WeaponTrailContainer _weaponTrailContainer;
        private Weapon _weapon;
        private AnimationHandler _animationHandler;
        private AttackStateComponentData _attackStateComponentData;

        private int _currentAttackIndex;
        private int _currentAttackAnimationIndex;
        private bool _isAttacking;
        private readonly List<string> _attacksAnimators = new List<string>() {"Attack", "Attack2"};
        private AttackEventListener _attackEventListener;
        private static readonly int IsAttack = Animator.StringToHash("IsAttacking");

        protected override void OnEnter()
        {
            _attackStateComponentData = _entity.Get<AttackStateComponentData>();
            _animationHandler ??= _entity.Get<AnimationHandler>();
            _weapon ??= _entity.Get<Weapon>();
            _weaponTrailContainer ??= _entity.Get<WeaponTrailContainer>();
            _attackEventListener ??= _entity.Get<AttackEventListener>();

            _animationHandler.Animator.SetBool(IsAttack, true);

            _weaponTrailContainer.XWeaponTrails.ForEach(x => x.Activate());
            _attackEventListener.AttackStarted += OnAttackStarted;
            _attackEventListener.AttackEnded += OnAttackEnded;
            _attackEventListener.StartListenCombo += OnStartListenCombo;
            _attackEventListener.StopListenCombo += OnStopListenCombo;
            Attack();
        }

        protected override void OnExit()
        {
            _entity.Remove<CurrentAttackData>();
            _entity.Get<ApplyRootMotionHandler>().SetAnimationRootMotionMultiplier(1);
            _attackEventListener.AttackStarted -= OnAttackStarted;
            _attackEventListener.AttackEnded -= OnAttackEnded;
            _attackEventListener.StartListenCombo -= OnStartListenCombo;
            _attackEventListener.StopListenCombo -= OnStopListenCombo;
            _weapon.Disable();
            _weaponTrailContainer.XWeaponTrails.ForEach(x => x.Deactivate());
            _currentAttackIndex = 0;
            _isAttacking = false;
            _animationHandler.Animator.SetBool(IsAttack, false);
        }

        protected override void OnUpdate()
        {
            if (_isAttacking == false &&
                _attackStateComponentData.ConditionsContainer.ContainerData.TrueForAll(x => x.GetStatus()))
            {
                Attack();
            }
        }

        private void Attack()
        {
            _isAttacking = true;
            Vector3 orientationForward = _attackStateComponentData.Orientation.forward;
            Vector3 orientationRight = _attackStateComponentData.Orientation.right;
            orientationForward.y = 0;
            orientationRight.y = 0;
            Vector3 moveDirection = orientationForward * Input.GetAxisRaw("Vertical") +
                                    orientationRight * Input.GetAxisRaw("Horizontal");


            _attackStateComponentData.WhoWasRotate.DORotateQuaternion(
                moveDirection != Vector3.zero
                    ? Quaternion.LookRotation(moveDirection)
                    : Quaternion.LookRotation(orientationForward), _attackStateComponentData.RotationSpeed);

            AttackDataInfo attackDataInfo = _weapon.CombatMoveSetSetup[_currentAttackIndex];
            _entity.AddOrGet<CurrentAttackData>().CurrentAttackDataInfo = attackDataInfo;
            _entity.Get<ApplyRootMotionHandler>()
                .SetAnimationRootMotionMultiplier(attackDataInfo
                    .RootMultiplierBeforeEndAttack);

            _animationHandler.OverrideAnimation(_attacksAnimators[_currentAttackAnimationIndex],
                attackDataInfo.AnimationClip);

            _animationHandler.Animator.SetTrigger(_attacksAnimators[_currentAttackAnimationIndex++]);

            _currentAttackAnimationIndex %= _attacksAnimators.Count;
        }

        private void OnAttackStarted()
        {
            _weapon.Enable(_weapon.CombatMoveSetSetup[_currentAttackIndex]);
        }

        private void OnAttackEnded()
        {
            _entity.Get<ApplyRootMotionHandler>()
                .SetAnimationRootMotionMultiplier(_weapon.CombatMoveSetSetup[_currentAttackIndex]
                    .RootMultiplierAfterEndAttack);

             _weapon.Disable();
        }

        private void OnStartListenCombo()
        {
            _isAttacking = false;
            _currentAttackIndex++;
            _currentAttackIndex %= _weapon.CombatMoveSetSetup.Count;
        }

        private void OnStopListenCombo()
        {
            _currentAttackIndex = 0;
        }
    }
}