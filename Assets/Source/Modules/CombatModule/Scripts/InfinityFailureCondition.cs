using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;

namespace Source.Modules.CombatModule.Scripts
{
    public class InfinityFailureCondition : IGameCondition
    {
        public TaskStatus GetConditionStatus()
        {
            return TaskStatus.Failure;
        }
    }
}