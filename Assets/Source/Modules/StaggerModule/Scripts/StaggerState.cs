using Source.Scripts;
using Source.Scripts.EntityLogic;

namespace Source.Modules.StaggerModule.Scripts
{
    public class StaggerState : State
    {
        private StaggerHandler _staggerHandler;
        private void OnEnable()
        {
            _staggerHandler ??= new StaggerHandler();
            _staggerHandler.Initialize(_entity);
            _entity.Add(_staggerHandler);
        }

        private void OnDisable()
        {
            _staggerHandler.SetLayersWeight(0, 0);
            _entity.Remove<StaggerHandler>();
        }
    }
}
