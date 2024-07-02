using System;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts;
using Source.Scripts.EntityLogic;

namespace Source.Modules.CombatModule.Scripts.Protection
{
    public class EndProtectionState : State
    {
        protected override void OnExit()
        {
            _entity.AddOrGet<EntityCurrentStatsData>().DamageReducePercent = 0;
        }
    }
}