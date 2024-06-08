using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class WaitAction : IGameAction
    {
        public TaskStatus ExecuteAction()
        {
            return TaskStatus.Running;
        }
    }
}