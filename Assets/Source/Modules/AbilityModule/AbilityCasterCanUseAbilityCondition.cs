using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Abilities;
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