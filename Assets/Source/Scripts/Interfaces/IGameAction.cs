using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace Source.Scripts.Interfaces
{
    public interface IGameAction
    {
        public TaskStatus ExecuteAction();
        public void OnStart(){}
        public void OnExit(){}
    }
}
