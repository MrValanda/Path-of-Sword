using BehaviorDesigner.Runtime.Tasks;

namespace Source.Scripts.Interfaces
{
    public interface IGameCondition
    {
        public void InitData(){}
        public TaskStatus GetConditionStatus();
    }
}