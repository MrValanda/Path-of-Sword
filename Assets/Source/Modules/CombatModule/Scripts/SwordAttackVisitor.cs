using System;
using System.Linq;
using Interfaces;
using Source.Scripts.EntityDataComponents;
using Source.Scripts.EntityLogic;
using Source.Scripts.Setups.Characters;
using Source.Scripts.VisitableComponents;
using UnityEngine;
using Animation = Source.Scripts.Enemy.Animation;

namespace Source.Scripts.Visitors
{
    [Serializable]
    public class SwordAttackVisitor : IVisitor
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private DamageableContainerSetup _damageableContainerSetup;
        [SerializeField] private float _damage;

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

            healthComponent.ApplyDamage(_damage);
        }

        public void Visit(Animation animation)
        {
        }
    }
}