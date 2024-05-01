using System.Linq;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups.Characters;
using UnityEngine;

namespace Source.Scripts.GameExtensions
{
    public static class MonoBehaviorExtension
    {
        public static bool CanAttackUnit<T>(this T unit, DamageableContainerSetup damageableContainerSetup)
            where T : Component
        {
            return unit.TryGetComponent(out IDamageable damageable) &&
                   damageableContainerSetup.DamageableTypes.Any(damageableType =>
                       damageableType.Type.Equals(damageable.GetType().Name));
        }
    }
}