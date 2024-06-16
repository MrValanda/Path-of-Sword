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
    [Serializable]
    public struct MoveSeTypeData
    {
        public MoveSetType MoveSetType;
        public CombatMoveSetSetup CombatMoveSetSetup;
    }

    public partial class Weapon : MonoBehaviour
    {
        [SerializeField] private Collider _weaponHitBoxes;
        [SerializeField] private List<MoveSeTypeData> _combatMoveSetSetup;

        private readonly Dictionary<HitBox, EntityAttackData> _entityAttackDatas = new(111);

        private IDisposable _colliderTriggerDisposable;
        private SwordAttackVisitor _swordAttackVisitor;
        private Transform _orientation;
        private HitBox _senderHitBox;

        public void Initialize(SwordAttackVisitor swordAttackVisitor, Transform orientation, HitBox senderHitBox)
        {
            _swordAttackVisitor = swordAttackVisitor;
            _orientation = orientation;
            _senderHitBox = senderHitBox;
        }

        private void OnDisable()
        {
            Disable();
        }

        public CombatMoveSetSetup this[MoveSetType moveSetType] =>
            _combatMoveSetSetup.Find(x => x.MoveSetType == moveSetType).CombatMoveSetSetup;

        public void Enable(HitInfo currentHitDataInfo)
        {
            _entityAttackDatas.Clear();
            _weaponHitBoxes.enabled = true;

            _colliderTriggerDisposable = _weaponHitBoxes.OnTriggerStayAsObservable().Subscribe(
                x => { ExecuteAttack(currentHitDataInfo, x); });
        }

        public void Disable()
        {
            _weaponHitBoxes.enabled = false;
            _entityAttackDatas.Clear();
            _colliderTriggerDisposable?.Dispose();
        }

        private void ExecuteAttack(HitInfo currentAttackDataInfo, Collider triggerCollider)
        {
            if (triggerCollider.TryGetComponent(out HitBox hitBox) == false ||
                ReferenceEquals(hitBox, _senderHitBox)) return;
            
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
            Debug.LogError("ExecuteAttack " + hitBox.name);
        }
    }
}