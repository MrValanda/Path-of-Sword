using System;
using System.Collections.ObjectModel;
using System.Linq;
using Source.Scripts.InterfaceLinker;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups.Characters;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Source.Modules.DamageableFindersModule
{
    public class DamageableFinder : IDisposable
    {
        private readonly DamageableContainerSetup _damageableContainerSetup;
        private readonly CompositeDisposable _compositeDisposable;
        public ObservableCollection<DamageableLinker> FindedDamageables { get; private set; } = new();

        public ObservableCollection<Transform> FindedDamageablesTransforms { get; private set; } = new();
        
        public DamageableFinder(DamageableContainerSetup damageableContainerSetup, Collider detectCollider)
        {
            _damageableContainerSetup = damageableContainerSetup;
            _compositeDisposable = new CompositeDisposable();
            _compositeDisposable.Add(detectCollider.OnTriggerEnterAsObservable().Subscribe(OnTriggerEnter));
            _compositeDisposable.Add(detectCollider.OnTriggerExitAsObservable().Subscribe(OnTriggerExit));
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DamageableLinker damageable) &&
                _damageableContainerSetup.DamageableTypes.Any(x =>
                    x.Type.Equals(damageable.Value.GetType().Name)))
            {
                if (damageable.Value is IDying dyingDamageable)
                {
                    dyingDamageable.Dead += OnDamageableDead;
                }

                FindedDamageables.Add(damageable);
                FindedDamageablesTransforms.Add(damageable.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out DamageableLinker damageable))
            {
                FindedDamageables.Remove(damageable);
                FindedDamageablesTransforms.Remove(damageable.transform);
            }
        }

        private void OnDamageableDead(IDying dyingDamageable)
        {
            DamageableLinker damageableLinker = FindedDamageables
                .FirstOrDefault(x => x.Value.Equals(dyingDamageable as IDamageable));

            FindedDamageables.Remove(damageableLinker);
            FindedDamageablesTransforms.Remove(damageableLinker?.transform);

            dyingDamageable.Dead -= OnDamageableDead;
        }
    }
}