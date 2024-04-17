using System;
using Lean.Pool;
using Source.Scripts.Abilities;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using Source.Scripts.Setups.Characters;
using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    [Serializable]
    public class SpawnAbilityAnimation : IAbilityAction
    {
        [SerializeField] private AbilityAnimation _abilityAnimation;
        [SerializeField] private DamageableContainerSetup _damageableContainerSetup;
        [SerializeField] private float _animationSpeed;
        
        public void ExecuteAction(Transform castPoint, Enemy.Enemy abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            abilityCaster.UpdateDamage(baseAbilitySetup.Damage);
            
            AbilityAnimation abilityAnimation = LeanPool.Spawn(_abilityAnimation, castPoint.position, castPoint.rotation);
            
            abilityAnimation.Init(_damageableContainerSetup, abilityCaster.CurrentDamage);
            abilityAnimation.ShowAnimation(_animationSpeed);
        }
    }
}