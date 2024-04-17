using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.BehaviorsNodes.SharedVariables;

namespace Source.Scripts.BehaviorsNodes.Actions
{
    public class MoveByDirection : Action
    {
        public SharedVector3 Direction;
        public SharedEnemyMovement SharedEnemyMovement;

        public override TaskStatus OnUpdate()
        {
            SharedEnemyMovement.Value.Move(Direction.Value);
            return TaskStatus.Success;
        }
    }
}