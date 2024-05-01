using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.BehaviorsNodes.SharedVariables;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Scripts.BehaviorsNodes.Actions
{
    public class CalculateDirectionToAttackPoint : Action
    {
        public SharedVector3 Direction;
        public SharedAttackPointCalculatorLinker AttackPointCalculatorLinker;

        public override TaskStatus OnUpdate()
        {
            Direction.Value = AttackPointCalculatorLinker.Value.Value.GetDirection();
            return TaskStatus.Success;
        }
    }
}