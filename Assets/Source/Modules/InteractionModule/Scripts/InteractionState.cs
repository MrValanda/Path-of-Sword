using Source.Scripts;
using Source.Scripts.EntityLogic;

namespace Source.Modules.InteractionModule.Scripts
{
    public class InteractionState : State
    {
        protected override void OnEnter()
        {
            _entity.Get<InteractionSelector>()?.SelectedInteraction.Interact(_entity);
        }
    }
}
