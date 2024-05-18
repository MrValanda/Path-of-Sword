using System;
using System.Collections.Generic;
using Source.Modules.CombatModule.Scripts;
using Source.Modules.MoveSetModule.Scripts;
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
                    AttackAction(hitBox);
                    _entityAttackDatas[hitBox].AttackHits++;
                    _entityAttackDatas[hitBox].LastAttackTime = Time.time;
                }
            }
            else
            {
                AttackAction(hitBox);
                _entityAttackDatas.Add(hitBox,
                    new EntityAttackData() {AttackHits = 1, LastAttackTime = Time.time});
            }
           
        }

        private void AttackAction(HitBox hitBox)
        {
            hitBox.Accept(_swordAttackVisitor);
            Vector3 direction = hitBox.transform.InverseTransformDirection(_orientation.forward);
            direction.y = 0;
            direction.x = Math.Clamp(direction.x * 10, -1, 1);
            direction.z = Math.Clamp(direction.z * 10, -1, 1);
            ImpactDirectionVisitor impactDirectionVisitor =
                new ImpactDirectionVisitor(new Vector2(direction.x, direction.z));
            hitBox.Accept(impactDirectionVisitor);
        }
    }
}