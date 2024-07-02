using Source.Modules.HealthModule.Scripts;
using Source.Scripts;
using Source.Scripts.EntityLogic;

namespace Source.Modules.CombatModule.Scripts.Protection
{
    public class StopProtectionState : State
    {
        private void OnEnable()
        {
            _entity.AddOrGet<EntityCurrentStatsData>().DamageReducePercent = 0;
        }
    }
}