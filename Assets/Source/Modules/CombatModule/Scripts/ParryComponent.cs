using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    public class ParryComponent
    {
        private static readonly int Parry = Animator.StringToHash("Parry");
        private static readonly int ParryBroken = Animator.StringToHash("ParryBroken");
        private static readonly int ParryIndex = Animator.StringToHash("ParryIndex");
        
        public Entity WhoParryEntity;
        public Entity WhomParryEntity;

        public void Execute()
        {
            WhoParryEntity.transform.LookAt(WhomParryEntity.transform);
            Quaternion transformRotation = WhoParryEntity.transform.rotation;
            transformRotation.x = transformRotation.z = 0;
            WhoParryEntity.transform.rotation = transformRotation;
            Animator animator = WhoParryEntity.Get<AnimationHandler>().Animator;
            animator.SetTrigger(Parry);
            animator.SetFloat(ParryIndex, (float)WhomParryEntity.Get<CurrentAttackData>().CurrentAttackDataInfo.ParryAnimationIndex);
          
            // WhomParryEntity.Get<AnimationHandler>().Animator.SetTrigger(ParryBroken);
            // WhomParryEntity.Add(new ParryBrokenComponent()
            //     {WhoBrokenParry = WhoParryEntity, WhomBrokenParry = WhomParryEntity});
            //
        }
    }

    public class ParryBrokenComponent
    {
        public Entity WhoBrokenParry;
        public Entity WhomBrokenParry;


        public void Execute()
        {
            
        }
    }
}