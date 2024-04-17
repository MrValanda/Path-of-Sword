using System.Linq;
using Interfaces;
using Source.Scripts.Setups.Characters;
using Source.Scripts.VisitableComponents;
using UnityEngine;
using VisitableComponents;
using Animation = Source.Scripts.Enemy.Animation;

namespace Visitors
{
    [CreateAssetMenu(fileName = "SwordAttackVisitor", menuName = "Visitors/SwordAttackVisitor")]
    public class SwordAttackVisitor : ScriptableObject, IVisitor
    {
        [SerializeField] private DamageableContainerSetup _damageableContainerSetup;
        [SerializeField] private float _damage;

        public void Visit(HealthComponent healthComponent)
        {
            if (_damageableContainerSetup.DamageableTypes.Any(x => x.Type.Equals(healthComponent.GetType().Name)) ==
                false) return;
            
            healthComponent.ApplyDamage(_damage);
        }

        public void Visit(Animation animation)
        {
            
        }
    }
}