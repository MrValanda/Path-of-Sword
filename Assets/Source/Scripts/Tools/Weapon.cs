using System;
using Source.Scripts.Visitors;
using Tools;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Source.Scripts.Tools
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private SwordAttackVisitor _swordAttackVisitor;
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _orientation;

        private IDisposable _disposable;

        public void Enable()
        {
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
            _collider.enabled = true;
        }

        public void Disable()
        {
            _disposable?.Dispose();
            _collider.enabled = false;
        }
    }
}