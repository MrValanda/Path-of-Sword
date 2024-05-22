using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.BehaviorsNodes.SharedVariables;
using Source.Scripts.Interfaces;
using Source.Scripts_DONT_USE_THIS_FOLDER_.BehaviorsNodes.SharedVariables;

namespace Source.Scripts.BehaviorsNodes.Actions
{
    public class Death : Action
    {
        public SharedGameActionsContainer DeathSharedGameActionsContainer;

        public override TaskStatus OnUpdate()
        {
            foreach (IGameAction gameAction in DeathSharedGameActionsContainer.Value)
            {
                gameAction.ExecuteAction();
            }

            return TaskStatus.Success;
        }
    }
}