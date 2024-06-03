using Source.Scripts;
using Source.Scripts.EntityLogic;

namespace Source.Modules.CombatModule.Scripts.Parry
{
    public class RemoveParryComponentState : State
    {
        protected override void OnEnter()
        {
            _entity.Remove<ParryComponent>();
        }
    }
}