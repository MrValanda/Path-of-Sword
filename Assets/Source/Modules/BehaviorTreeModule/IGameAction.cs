using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace Source.Modules.BehaviorTreeModule
{
    public interface IGameAction
    {
        public TaskStatus ExecuteAction();
        public void OnStart(){}
        public void OnExit(){}
        public void OnConditionAbort(){}
    }
}
