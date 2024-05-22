using System;
using System.Linq;
using Interfaces;
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

        public void Initialize(Entity entity,DamageableContainerSetup damageableContainerSetup)
        {
            _entity = entity;
            _damageableContainerSetup = damageableContainerSetup;
        }
        
        public void Visit(HealthComponent healthComponent)
        {
            if (_damageableContainerSetup.DamageableTypes.
                    Any(x => x.Type.Equals(healthComponent.GetType().Name)) == false)
                return;

            if (healthComponent.Entity.TryGet(out ParryComponent parryComponent))
            {
                parryComponent.WhomParryEntity = _entity;
                parryComponent.Execute();
                return;
            }

            healthComponent.ApplyDamage(_entity.Get<CurrentAttackData>().CurrentHitInfo.Damage);
        }

        public void Visit(Animation animation)
        {
        }
    }
}