using Source.Modules.CombatModule.Scripts.Parry;
using Source.Scripts;
using Source.Scripts.EntityLogic;

namespace Source.Modules.CombatModule.Scripts
{
    public class ParryBrokenState : State
    {
        protected override void OnExit()
        {
            _entity.Remove<ParryBrokenComponent>();
        }
    }
}
