using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.EntityLogic;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class DisableEntityComponent<T> : IGameAction where T: class
    {
        private readonly Entity _entity;

        private T _disabledComponent;

        public DisableEntityComponent(Entity entity)
        {
            _entity = entity;
        }

        public void OnStart()
        {
            if (_entity.Contains<T>())
            {
                _disabledComponent = _entity.Get<T>();
                _entity.Remove<T>();
            }
        }

        public void OnExit()
        {
            if(_disabledComponent == null) return;

            _entity.Add(_disabledComponent);
        }

        public TaskStatus ExecuteAction()
        {
            return TaskStatus.Success;
        }
    }
}