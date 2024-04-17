using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.BehaviorsNodes.SharedVariables;
using Source.Scripts.Interfaces;

namespace Source.Scripts.BehaviorsNodes.Actions
{
    public class Death : Action
    {
        public SharedGameActionsContainer DeathSharedGameActionsContainer;

        public override TaskStatus OnUpdate()
        {
            foreach (IGameAction gameAction in DeathSharedGameActionsContainer.Value.GameActions)
            {
                gameAction.ExecuteAction();
            }

            return TaskStatus.Success;
        }
    }
}