using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.Interfaces;

namespace Source.Modules.CombatModule.Scripts
{
    [Serializable]
    public class InfinitySuccessCondition : IGameCondition
    {
        public TaskStatus GetConditionStatus()
        {
            return TaskStatus.Success;
        }
    }
}