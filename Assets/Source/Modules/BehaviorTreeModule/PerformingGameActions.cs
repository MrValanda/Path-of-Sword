using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts_DONT_USE_THIS_FOLDER_.BehaviorsNodes.SharedVariables;
using UnityEngine.Scripting;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Modules.BehaviorTreeModule
{
    [Preserve]
    public class PerformingGameActions : Action
    {
        public SharedGameActionsContainer AttackSharedGameActionsContainer;

        public override void OnStart()
        {
            foreach (IGameAction valueGameAction in AttackSharedGameActionsContainer.Value)
            {
                valueGameAction.OnStart();
            }
        }

        public override void OnEnd()
        {
            foreach (IGameAction valueGameAction in AttackSharedGameActionsContainer.Value)
            {
                valueGameAction.OnExit();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (AttackSharedGameActionsContainer.Value.Count == 0) return TaskStatus.Success;
            TaskStatus taskStatus = TaskStatus.Inactive;
            foreach (IGameAction gameAction in AttackSharedGameActionsContainer.Value)
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