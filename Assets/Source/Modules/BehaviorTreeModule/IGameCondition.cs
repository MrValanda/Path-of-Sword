using BehaviorDesigner.Runtime.Tasks;

namespace Source.Modules.BehaviorTreeModule
{
    public interface IGameCondition
    {
        public void InitData(){}
        public TaskStatus GetConditionStatus();
    }
}