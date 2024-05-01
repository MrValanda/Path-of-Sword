using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.Abilities;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameConditionals
{
    [Serializable]
    public class AbilityProcessingCondition : IGameCondition
    {
        [SerializeField] private AbilityCaster _abilityCaster;

        public AbilityProcessingCondition(AbilityCaster abilityCaster)
        {
            _abilityCaster = abilityCaster;
        }

        public TaskStatus GetConditionStatus()
        {
            return _abilityCaster.IsAbilityProcessed ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}