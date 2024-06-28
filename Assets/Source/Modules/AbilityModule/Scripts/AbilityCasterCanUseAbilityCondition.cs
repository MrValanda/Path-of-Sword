using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.AbilityModule.Scripts;
using Source.Modules.BehaviorTreeModule;
using UnityEngine;

namespace Source.Scripts.GameConditionals
{
    [Serializable]
    public class AbilityCasterCanUseAbilityCondition : IGameCondition
    {
        [SerializeField] private AbilityCaster _abilityCaster;

        public AbilityCasterCanUseAbilityCondition(AbilityCaster abilityCaster)
        {
            _abilityCaster = abilityCaster;
        }

        public TaskStatus GetConditionStatus()
        {
            return _abilityCaster.CanUseAbility() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}