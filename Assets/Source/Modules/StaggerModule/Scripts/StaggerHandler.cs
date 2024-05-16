using System;
using Source.Modules.CombatModule.Scripts;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using Source.Scripts.VisitableComponents;
using UnityEngine;

namespace Source.Modules.StaggerModule.Scripts
{
    public class StaggerHandler : IDisposable
    {
        private static readonly string ImpactLayerName = "Impact";
        private static readonly string ProtectionImpactLayerName = "ProtectionImpact";
        
        private Entity _entity;
        private float _lastReceivedDamage;
        private Animator _animator;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _animator ??= _entity.Get<AnimationHandler>().Animator;
            HealthComponent healthComponent = _entity.Get<HealthComponent>();
            healthComponent.ReceivedDamage += OnReceivedDamage;
        }

        public void Dispose()
        {
            HealthComponent healthComponent = _entity.Get<HealthComponent>();
            healthComponent.ReceivedDamage -= OnReceivedDamage;
        }

        private void OnReceivedDamage(double obj)
        {
            if (_entity.Get<EntityCurrentStatsData>().DamageReducePercent > 0)
            {
                SetLayersWeight(0, 1);
                _entity.Get<ParryEffectSpawner>().SpawnEffect();
            }
            else
            {
                if(Time.time - _lastReceivedDamage > 1)
                {
                    SetLayersWeight(1, 0);
                }
                else
                {
                    SetLayersWeight(1f, 0);
                }
                _lastReceivedDamage = Time.time;
            }
        }

        private void SetLayersWeight(float impactWeight,float protectionImpactWeight)
        {
            _animator.SetLayerWeight(_animator.GetLayerIndex(ImpactLayerName), impactWeight);
            _animator.SetLayerWeight(_animator.GetLayerIndex(ProtectionImpactLayerName), protectionImpactWeight);
        }
    }
}
