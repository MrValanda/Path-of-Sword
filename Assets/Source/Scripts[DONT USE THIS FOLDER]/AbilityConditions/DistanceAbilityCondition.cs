using System;
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

        public bool CanExecute(Transform castPoint, Enemy.Enemy abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            var distance = Vector3.Distance(abilityCaster.transform.position, abilityCaster.Target.transform.position);
            return distance <= _maxDistance && distance >= _minDistance;
        }
    }
}