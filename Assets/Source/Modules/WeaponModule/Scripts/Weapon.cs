using System;
using System.Collections.Generic;
using Source.Scripts.CombatModule;
using Source.Scripts.Visitors;
using Tools;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Source.Modules.WeaponModule.Scripts
{
    public partial class Weapon : MonoBehaviour
    {
        [SerializeField] private Collider _weaponHitBoxes;
        [field: SerializeField] public CombatMoveSetSetup CombatMoveSetSetup { get; private set; }

        private readonly Dictionary<HitBox, EntityAttackData> _entityAttackDatas = new(111);

        private IDisposable _colliderTriggerDisposable;
        private SwordAttackVisitor _swordAttackVisitor;
        private Transform _orientation;

        public void Initialize(SwordAttackVisitor swordAttackVisitor, Transform orientation)
        {
            _swordAttackVisitor = swordAttackVisitor;
            _orientation = orientation;
        }

        private void OnDestroy()
        {
            Disable();
        }

        public void Enable(AttackDataInfo currentAttackDataInfo)
        {
            _entityAttackDatas.Clear();
            _weaponHitBoxes.enabled = true;
         
            _colliderTriggerDisposable = _weaponHitBoxes.OnTriggerStayAsObservable().Subscribe(
                x =>
                {
                    ExecuteAttack(currentAttackDataInfo, x);

                   
                });
        }

        public void Disable()
        {
            _weaponHitBoxes.enabled = false;
            _entityAttackDatas.Clear();
            _colliderTriggerDisposable?.Dispose();
        }

        private void ExecuteAttack(AttackDataInfo currentAttackDataInfo, Collider triggerCollider)
        {
            if (triggerCollider.TryGetComponent(out HitBox hitBox) == false) return;

            if (_entityAttackDatas.ContainsKey(hitBox))
            {
                if (Time.time - _entityAttackDatas[hitBox].LastAttackTime >=
                    currentAttackDataInfo.DelayBetweenHits &&
                    _entityAttackDatas[hitBox].AttackHits < currentAttackDataInfo.NumberOfHitsPerUnit)
                {
                    hitBox.Accept(_swordAttackVisitor);
                    _entityAttackDatas[hitBox].AttackHits++;
                    _entityAttackDatas[hitBox].LastAttackTime = Time.time;
                }
            }
            else
            {
                hitBox.Accept(_swordAttackVisitor);

                _entityAttackDatas.Add(hitBox,
                    new EntityAttackData() {AttackHits = 1, LastAttackTime = Time.time});
            }
        }
    }
}