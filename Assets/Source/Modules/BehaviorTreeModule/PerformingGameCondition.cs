using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule.SharedVariables;

namespace Source.Modules.BehaviorTreeModule
{
    public class PerformingGameCondition : Conditional
    {
        public SharedGameConditionsContainer CanSeeSharedGameConditionsContainer;

        public override void OnStart()
        {
            foreach (IGameCondition valueGameCondition in CanSeeSharedGameConditionsContainer.Value)
            {
                valueGameCondition.InitData();
            }
        }

        public override TaskStatus OnUpdate()
        {
            foreach (IGameCondition valueGameCondition in CanSeeSharedGameConditionsContainer.Value)
            {
                if (valueGameCondition.GetConditionStatus() == TaskStatus.Failure)
                {
                    return TaskStatus.Failure;
                }
            }

            return TaskStatus.Success;
        }
    }
}