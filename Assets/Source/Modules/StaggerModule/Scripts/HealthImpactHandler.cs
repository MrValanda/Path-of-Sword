using System;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts.EntityLogic;

namespace Source.Modules.StaggerModule.Scripts
{
    public class HealthImpactHandler : IDisposable
    {        
        protected Entity Entity;

        public void Initialize(Entity entity)
        {
            Entity = entity;
            HealthComponent healthComponent = Entity.Get<HealthComponent>();
            healthComponent.ReceivedDamage += OnReceivedDamage;
            OnInitialized();
        }
        
        
        public void Dispose()
        {
            Entity.Get<HealthComponent>().ReceivedDamage -= OnReceivedDamage;
        }

        protected virtual void OnInitialized()
        {
            
        }
        protected virtual void OnReceivedDamage(double obj)
        {
        }
    }
}