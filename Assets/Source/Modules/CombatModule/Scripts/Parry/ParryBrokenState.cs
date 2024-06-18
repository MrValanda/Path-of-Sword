using Source.Modules.CombatModule.Scripts.Parry;
using Source.Modules.HealthModule.Scripts;
using Source.Modules.StaminaModule.Scripts;
using Source.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    public class ParryBrokenState : State
    {
        private static readonly int ParryBroken = Animator.StringToHash("ParryBroken");

        protected override void OnEnter()
        {
            _entity.AddOrGet<EntityCurrentStatsData>().DamageReducePercent = 0;
            _entity.Get<AnimationHandler>().Animator.SetTrigger(ParryBroken);
        }

        protected override void OnExit()
        {
            _entity.Get<StaminaModel>().UpdateStamina(111111);
            _entity.Remove<ParryBrokenComponent>();
        }
    }
}
