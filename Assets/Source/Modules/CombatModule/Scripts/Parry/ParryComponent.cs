using Source.Modules.MovementModule.Scripts;
using Source.Modules.MoveSetModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    public class ParryComponent
    {
        private const int Deceleration = 10;
        private static readonly int Parry = Animator.StringToHash("Parry");
        private static readonly int ParryBroken = Animator.StringToHash("ParryBroken");
        private static readonly int ParryIndex = Animator.StringToHash("ParryIndex");

        public Entity WhoParryEntity;
        public Entity WhomParryEntity;

        public void Execute()
        {
            HitInfo currentAttackDataInfo = WhomParryEntity.Get<CurrentAttackData>().CurrentHitInfo;
            WhoParryEntity.transform.LookAt(WhomParryEntity.transform);
            Quaternion transformRotation = WhoParryEntity.transform.rotation;
            transformRotation.x = transformRotation.z = 0;
            WhoParryEntity.transform.rotation = transformRotation;

            AddForceDirectionComponent addForceDirectionComponent =
                WhoParryEntity.AddOrGet<AddForceDirectionComponent>();
            
            addForceDirectionComponent.WhoWillMoveEntity = WhoParryEntity;
            addForceDirectionComponent.Execute(-WhoParryEntity.transform.forward * currentAttackDataInfo.ParryBackForce,
                Deceleration);

            WhoParryEntity.Get<ParryEffectSpawner>().SpawnEffect();
            WhoParryEntity.AddOrGet<ParryCompleteComponent>().WhomParryEntity = WhomParryEntity;
            WhoParryEntity.AddOrGet<ParryCompleteComponent>().WhoParryEntity = WhoParryEntity;
            
            Animator animator = WhoParryEntity.Get<AnimationHandler>().Animator;
            animator.SetTrigger(Parry);
            animator.SetFloat(ParryIndex, (float) currentAttackDataInfo.ParryAnimationIndex);

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