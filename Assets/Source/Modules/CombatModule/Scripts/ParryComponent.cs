using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    public class ParryComponent
    {
        public Entity WhoParryEntity;
        public Entity WhomParryEntity;
        private static readonly int Parry = Animator.StringToHash("Parry");
        private static readonly int ParryBroken = Animator.StringToHash("ParryBroken");

        public void Execute()
        {
            WhoParryEntity.transform.LookAt(WhomParryEntity.transform);
            WhoParryEntity.Get<AnimationHandler>().Animator.SetTrigger(Parry);
            WhomParryEntity.Get<AnimationHandler>().Animator.SetTrigger(ParryBroken);
            Debug.LogError("Parry");
        }
    }
}