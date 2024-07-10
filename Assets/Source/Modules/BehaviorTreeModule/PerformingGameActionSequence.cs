using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule.SharedVariables;
using UnityEngine.Scripting;

namespace Source.Modules.BehaviorTreeModule
{
    [Preserve]
    public class PerformingGameActionSequence : Action
    {
        public SharedGameActionsContainer SharedGameActionsContainer;
        
        private int _currentExecutedActionIndex;
        
        public override void OnStart()
        {
            _currentExecutedActionIndex = 0;
            foreach (IGameAction valueGameAction in SharedGameActionsContainer.Value)
            {
                valueGameAction.OnStart();
            }
        }

        public override void OnEnd()
        {
            foreach (IGameAction valueGameAction in SharedGameActionsContainer.Value)
            {
                valueGameAction.OnExit();
            }
        }

        public override void OnConditionalAbort()
        {
            foreach (IGameAction valueGameAction in SharedGameActionsContainer.Value)
            {
                valueGameAction.OnExit();
                valueGameAction.OnConditionAbort();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (SharedGameActionsContainer.Value.Count == 0) return TaskStatus.Success;

            TaskStatus executeAction = SharedGameActionsContainer[_currentExecutedActionIndex].ExecuteAction();
            if (executeAction == TaskStatus.Success)
            {
                _currentExecutedActionIndex++;
             
                return _currentExecutedActionIndex < SharedGameActionsContainer.Count
                    ? TaskStatus.Running
                    : TaskStatus.Success;
            }

            return executeAction;
        }
    }
}