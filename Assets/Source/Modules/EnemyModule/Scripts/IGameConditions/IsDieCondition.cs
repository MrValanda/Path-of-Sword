using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.HealthModule.Scripts;

namespace Source.Modules.EnemyModule.Scripts.IGameConditions
{
    public class IsDieCondition : IGameCondition
    {
        private readonly HealthComponent _healthComponent;

        public IsDieCondition(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        public TaskStatus GetConditionStatus()
        {
            return _healthComponent.IsDead ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}