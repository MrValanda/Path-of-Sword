using System;
using UnityEngine;

namespace Source.Modules.HealthModule.Scripts
{
    [Serializable]
    public class ReceiveDamageTransition : Transition
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private float _damageToTransit;

        public ReceiveDamageTransition(HealthComponent healthComponent, float damageToTransit)
        {
            _healthComponent = healthComponent;
            _damageToTransit = damageToTransit;
        }

        public override void OnEnable()
        {
            _healthComponent.ReceivedDamage += OnReceivedDamage;
        }

        public override void OnDisable()
        {
            _healthComponent.ReceivedDamage -= OnReceivedDamage;
        }

        private void OnReceivedDamage(double damage)
        {
            if (damage >= _damageToTransit)
            {
                OnNeedTransit(this);
            }
        }
    }
}