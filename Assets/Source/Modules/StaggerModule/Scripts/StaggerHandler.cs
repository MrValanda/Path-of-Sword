using System;
using Source.Modules.CombatModule.Scripts;
using Source.Modules.HealthModule.Scripts;
using Source.Modules.MovementModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.StaggerModule.Scripts
{
    public class StaggerHandler : IDisposable
    {
        private static readonly int ProtectionImpact = Animator.StringToHash("ProtectionImpact");
        private static readonly int Impact = Animator.StringToHash("Impact");

        private Entity _entity;
        private float _lastReceivedDamage;
        private Animator _animator;
        private string _impactLayerName = "Impact";
        public float ImpactWeight { get; private set; }

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _animator ??= _entity.Get<AnimationHandler>().Animator;
            HealthComponent healthComponent = _entity.Get<HealthComponent>();
            healthComponent.ReceivedDamage += OnReceivedDamage;
            ImpactWeight = 1;
        }

        public void Dispose()
        {
            HealthComponent healthComponent = _entity.Get<HealthComponent>();
            healthComponent.ReceivedDamage -= OnReceivedDamage;
        }

        public void SetImpactLayerName(string impactNameLayer)
        {
            _impactLayerName = impactNameLayer;
        }

        public void SetImpactWeight(float impactWeight)
        {
            ImpactWeight = impactWeight;
        }

        private void OnReceivedDamage(double obj)
        {
            Debug.LogError(_entity.Get<EntityCurrentStatsData>().DamageReducePercent);
            if (_entity.Get<EntityCurrentStatsData>().DamageReducePercent > 0)
            {
                _entity.Add(new ProtectionImpactOneFrame(_entity));
                _entity.Get<ProtectionSpawner>().SpawnEffect();
                if (_entity.Contains<DodgeTag>() == false)
                {
                    _animator.SetTrigger(ProtectionImpact);
                }
            }
            else
            {
                if (Time.time - _lastReceivedDamage > 1)
                {
                    SetLayersWeight(ImpactWeight);
                }
                else
                {
                    SetLayersWeight(ImpactWeight);
                }

                if (_entity.Contains<DodgeTag>() == false)
                {
                    _animator.SetTrigger(Impact);

                    if (_entity.Contains<ReceiveDamageSpawner>())
                    {
                        _entity.Get<ReceiveDamageSpawner>().SpawnEffect();
                    }
                }

                _lastReceivedDamage = Time.time;
            }
        }

        public void SetLayersWeight(float impactWeight)
        {
            _animator.SetLayerWeight(_animator.GetLayerIndex(_impactLayerName), impactWeight);
        }
    }
}