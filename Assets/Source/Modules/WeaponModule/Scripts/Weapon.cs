using System;
using Source.Scripts.CombatModule;
using Source.Scripts.Setups;
using Source.Scripts.Visitors;
using Tools;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Source.Modules.WeaponModule.Scripts
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public CombatMoveSetSetup CombatMoveSetSetup { get; private set; }
        
        [SerializeField] private Collider _collider;

        private IDisposable _disposable;
        private SwordAttackVisitor _swordAttackVisitor;
        private Transform _orientation;

        public void Initialize(SwordAttackVisitor swordAttackVisitor,Transform orientation)
        {
            _swordAttackVisitor = swordAttackVisitor;
            _orientation = orientation;
        }
        
        public void Enable()
        {
            _collider.enabled = true;
            if (_disposable != null) return;
             _disposable?.Dispose();
            _disposable = _collider.OnTriggerEnterAsObservable().Subscribe(x =>
            {
                if (x.TryGetComponent(out HitBox hitBox) == false) return;
                Vector3 direction = hitBox.transform.InverseTransformDirection(_orientation.forward);
                direction.y = 0;
                direction.x = Math.Clamp(direction.x * 10, -1, 1);
                direction.z = Math.Clamp(direction.z * 10, -1, 1);
                ImpactDirectionVisitor impactDirectionVisitor =
                    new ImpactDirectionVisitor(new Vector2(direction.x, direction.z));

                hitBox.Accept(_swordAttackVisitor);
                hitBox.Accept(impactDirectionVisitor);
            });
        }

        public void Disable()
        {
            _collider.enabled = false;
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}