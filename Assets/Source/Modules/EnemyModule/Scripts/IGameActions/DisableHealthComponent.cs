using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.HealthModule.Scripts;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class DisableHealthComponent : IGameAction
    {
        private readonly HealthComponent _healthComponent;

        public DisableHealthComponent(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        public void OnExit()
        {
            _healthComponent.enabled = true;
        }

        public TaskStatus ExecuteAction()
        {
            _healthComponent.enabled = false;
            return TaskStatus.Success;
        }
    }
}