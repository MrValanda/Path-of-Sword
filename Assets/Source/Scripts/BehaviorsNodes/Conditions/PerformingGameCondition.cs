using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.BehaviorsNodes.SharedVariables;
using Source.Scripts.Interfaces;

namespace Source.Scripts.BehaviorsNodes.Conditions
{
    public class PerformingGameCondition : Conditional
    {
        public SharedGameConditionsContainer CanSeeSharedGameConditionsContainer;

        public override void OnStart()
        {
            foreach (IGameCondition valueGameCondition in CanSeeSharedGameConditionsContainer.Value.GameConditions)
            {
                valueGameCondition.InitData();
            }
        }

        public override TaskStatus OnUpdate()
        {
            foreach (IGameCondition valueGameCondition in CanSeeSharedGameConditionsContainer.Value.GameConditions)
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