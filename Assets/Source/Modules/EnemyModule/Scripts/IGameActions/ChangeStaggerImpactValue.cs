using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.StaggerModule.Scripts;
using Source.Scripts.EntityLogic;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class ChangeStaggerImpactValue : IGameAction
    {
        private readonly Entity _entity;
        private readonly float _staggerImpactValue;
        private float _previousImpactValue;

        public ChangeStaggerImpactValue(Entity entity,float staggerImpactValue)
        {
            _entity = entity;
            _staggerImpactValue = staggerImpactValue;
        }

        public void OnStart()
        {
            _previousImpactValue = _entity.Get<StaggerHandler>().ImpactWeight;
            _entity.Get<StaggerHandler>().SetImpactWeight(_staggerImpactValue);
        }

        public void OnExit()
        {
            _entity.Get<StaggerHandler>().SetImpactWeight(_previousImpactValue);
        }

        public TaskStatus ExecuteAction()
        {
            return TaskStatus.Success;
        }
    }
}