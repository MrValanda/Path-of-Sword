using System;
using System.Collections.Generic;
using Source.Scripts.CombatModule;
using Source.Scripts.Visitors;
using Tools;
using UniRx;
using UnityEngine;

namespace Source.Modules.WeaponModule.Scripts
{
    public partial class Weapon : MonoBehaviour
    {
        [SerializeField] private List<WeaponHitBox> _weaponHitBoxes;
        [field: SerializeField] public CombatMoveSetSetup CombatMoveSetSetup { get; private set; }
        
        private readonly Dictionary<HitBox, EntityAttackData> _entityAttackDatas = new(111);

        private IDisposable _disposable;
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
            Disable();
            _disposable = Observable.EveryFixedUpdate().Subscribe(
                    x =>
                    {
                        ExecuteAttack(currentAttackDataInfo);

                        //    if (x.TryGetComponent(out HitBox hitBox) == false) return;
                        // Vector3 direction = hitBox.transform.InverseTransformDirection(_orientation.forward);
                        // direction.y = 0;
                        // direction.x = Math.Clamp(direction.x * 10, -1, 1);
                        // direction.z = Math.Clamp(direction.z * 10, -1, 1);
                        // ImpactDirectionVisitor impactDirectionVisitor =
                        //     new ImpactDirectionVisitor(new Vector2(direction.x, direction.z));
                        //
                        // hitBox.Accept(_swordAttackVisitor);
                        // hitBox.Accept(impactDirectionVisitor);
                    });
        }

        public void Disable()
        {
            _entityAttackDatas.Clear();
            _disposable?.Dispose();
        }

        private void ExecuteAttack(AttackDataInfo currentAttackDataInfo)
        {
            foreach (WeaponHitBox weaponHitBox in _weaponHitBoxes)
            {
                foreach (ColliderHit colliderHit in weaponHitBox.ExecuteHit(111))
                {
                    if (colliderHit.collider.TryGetComponent(out HitBox hitBox) == false) continue;

                    if (_entityAttackDatas.ContainsKey(hitBox))
                    {
                        if (Time.time - _entityAttackDatas[hitBox].LastAttackTime >=
                            currentAttackDataInfo.DelayBetweenHits &&
                            _entityAttackDatas[hitBox].AttackHits < currentAttackDataInfo.NumberOfHitsPerUnit)
                        {
                            hitBox.Accept(_swordAttackVisitor);
                            Debug.LogError("ACCEPT");
                            _entityAttackDatas[hitBox].AttackHits++;
                            _entityAttackDatas[hitBox].LastAttackTime = Time.time;
                        }
                    }
                    else
                    {
                        hitBox.Accept(_swordAttackVisitor);
                        Debug.LogError("ACCEPT");

                        _entityAttackDatas.Add(hitBox,
                            new EntityAttackData() {AttackHits = 1, LastAttackTime = Time.time});
                    }
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            foreach (WeaponHitBox weaponHitBox in _weaponHitBoxes)
            {
                Gizmos.DrawWireSphere(weaponHitBox.hitBoxPoint.position, weaponHitBox.radius  * weaponHitBox.lossyScaleFactor.lossyScale.x);
            }
        }
#endif
    }
}