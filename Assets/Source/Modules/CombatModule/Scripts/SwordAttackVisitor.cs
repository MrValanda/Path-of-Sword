using System;
using System.Linq;
using Interfaces;
using Source.Modules.CombatModule.Scripts.Parry;
using Source.Modules.HealthModule.Scripts;
using Source.Modules.MovementModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Setups.Characters;
using Source.Scripts.VisitableComponents;
using UnityEngine;
using Animation = Source.Scripts.Enemy.Animation;

namespace Source.Modules.CombatModule.Scripts
{
    [Serializable]
    public class SwordAttackVisitor : IVisitor
    {
        [SerializeField] private Entity _entity;

        private DamageableContainerSetup _damageableContainerSetup;

        public void Initialize(Entity entity, DamageableContainerSetup damageableContainerSetup)
        {
            _entity = entity;
            _damageableContainerSetup = damageableContainerSetup;
        }

        public void Visit(HealthComponent healthComponent)
        {
            if ( healthComponent.IsDead)
            {
                 return;
            }
            if (_damageableContainerSetup.DamageableTypes.Any(x => x.Type.Equals(healthComponent.GetType().Name)) ==
                false)
                return;

            Debug.LogError(healthComponent.Entity.Contains<ParryComponent>());
            if (healthComponent.Entity.TryGet(out ParryComponent parryComponent))
            {
                parryComponent.WhomParryEntity = _entity;
                parryComponent.Execute();
                return;
            }

            AddForceDirectionComponent addForceDirectionComponent =
                healthComponent.Entity.AddOrGet<AddForceDirectionComponent>();
            addForceDirectionComponent.WhoWillMoveEntity = healthComponent.Entity;
            addForceDirectionComponent.Execute(_entity.transform.forward *
                                               _entity.Get<CurrentAttackData>().CurrentHitInfo.ParryBackForce, 10f);
            healthComponent.ApplyDamage(_entity.Get<CurrentAttackData>().CurrentHitInfo.Damage);
        }

        public void Visit(Animation animation)
        {
        }
    }
}