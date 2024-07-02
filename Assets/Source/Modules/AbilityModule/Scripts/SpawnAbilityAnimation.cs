using System;
using Lean.Pool;
using Source.Modules.EnemyModule.Scripts;
using Source.Scripts.Abilities;
using Source.Scripts.EntityLogic;
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
        
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            AbilityAnimation abilityAnimation = LeanPool.Spawn(_abilityAnimation, castPoint.position, castPoint.rotation);

            abilityAnimation.Init(_damageableContainerSetup,
                abilityCaster.Get<DamageCalculator>().CalculateValue(baseAbilitySetup.Damage));
            abilityAnimation.ShowAnimation(_animationSpeed);
        }
    }
}