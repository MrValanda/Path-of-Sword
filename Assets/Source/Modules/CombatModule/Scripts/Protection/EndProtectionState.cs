using Source.Modules.HealthModule.Scripts;
using Source.Scripts;
using Source.Scripts.EntityLogic;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.States
{
    public class EndProtectionState : State
    {
        protected override void OnExit()
        {
            _entity.AddOrGet<EntityCurrentStatsData>().DamageReducePercent = 0;
        }
    }
}