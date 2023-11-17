using Interfaces;
using UnityEngine;
using VisitableComponents;

namespace Visitors
{
    [CreateAssetMenu(fileName = "SwordAttackVisitor", menuName = "Visitors/SwordAttackVisitor")]
    public class SwordAttackVisitor : ScriptableObject, IVisitor
    {
        [SerializeField] private float _damage;

        public void Visit(HealthComponent healthComponent)
        {
            healthComponent.ApplyDamage(_damage);
        }
    }
}