using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.BehaviorsNodes.SharedVariables;
using Source.Scripts.Interfaces;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Scripts.BehaviorsNodes.Actions
{
    public class PerformingGameActions : Action
    {
        public SharedGameActionsContainer AttackSharedGameActionsContainer;

        public override void OnStart()
        {
            foreach (IGameAction valueGameAction in AttackSharedGameActionsContainer.Value.GameActions)
            {
                valueGameAction.OnStart();
            }
        }

        public override void OnEnd()
        {
            foreach (IGameAction valueGameAction in AttackSharedGameActionsContainer.Value.GameActions)
            {
                valueGameAction.OnExit();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (AttackSharedGameActionsContainer.Value.GameActions.Count == 0) return TaskStatus.Success;
            TaskStatus taskStatus = TaskStatus.Inactive;
            foreach (IGameAction gameAction in AttackSharedGameActionsContainer.Value.GameActions)
            {
                TaskStatus executeActionStatus = gameAction.ExecuteAction();
                switch (executeActionStatus)
                {
                    case TaskStatus.Inactive:
                        break;
                    case TaskStatus.Failure:
                    {
                        taskStatus = executeActionStatus;
                    }
                        break;
                    case TaskStatus.Success:
                    {
                        if (taskStatus != TaskStatus.Running && taskStatus != TaskStatus.Failure)
                        {
                            taskStatus = executeActionStatus;
                        }
                    }
                        break;
                    case TaskStatus.Running:
                    {
                        if (taskStatus != TaskStatus.Failure)
                        {
                            taskStatus = executeActionStatus;
                        }
                    }
                        break;
                }
            }

            return taskStatus;
        }
    }
}