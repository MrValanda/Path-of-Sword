using System;
using Source.Scripts.VisitableComponents;

namespace Source.Modules.HealthModule.Scripts
{
    public class HealthController : IDisposable
    {
        private readonly HealthComponent _healthComponent;
        private readonly HealthView _healthView;

        public HealthController(HealthView healthView,HealthComponent healthComponent)
        {
            _healthView = healthView;
            _healthComponent = healthComponent;
            _healthComponent.ReceivedDamage += OnReceivedDamage;
            _healthView.UpdateValue(1);
        }
        
        public void Dispose()
        {
            _healthComponent.ReceivedDamage -= OnReceivedDamage;
        }

        private void OnReceivedDamage(double damage)
        {
            _healthView.UpdateValue((float) (_healthComponent.CurrentHealth / _healthComponent.MaxHealth));
        }
    }
}