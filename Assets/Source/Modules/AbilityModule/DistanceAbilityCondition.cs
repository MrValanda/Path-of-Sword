using System;
using Source.Modules.DamageableFindersModule;
using Source.Scripts.EntityLogic;
using Source.Scripts.GameConditionals;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.AbilityConditions
{
    [Serializable]
    public class DistanceAbilityCondition : IAbilityCondition
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _minDistance;

        public bool CanExecute(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            if (abilityCaster.Get<DamageableSelector>().SelectedDamageable == null) return false;
            var distance = Vector3.Distance(abilityCaster.transform.position,
                abilityCaster.Get<DamageableSelector>().SelectedDamageable.transform.position);
            return distance <= _maxDistance && distance >= _minDistance;
        }
    }
}