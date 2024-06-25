using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;

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