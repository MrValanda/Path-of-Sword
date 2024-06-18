using System;
using Source.CodeLibrary.ServiceBootstrap;
using Source.Modules.CameraModule.Scripts;
using Source.Modules.MovementModule.Scripts;
using Source.Modules.MoveSetModule.Scripts;
using Source.Modules.StaminaModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UniRx;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts.Parry
{
    public class ParryComponent
    {
        private const int Deceleration = 10;
        private static readonly int Parry = Animator.StringToHash("Parry");
        private static readonly int ParryBroken = Animator.StringToHash("ParryBroken");
        private static readonly int ParryIndex = Animator.StringToHash("ParryIndex");

        public Entity WhoParryEntity;
        public Entity WhomParryEntity;
        private IDisposable _disposable;
        
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
            _disposable?.Dispose();
            
            WhoParryEntity.Get<ParryEffectSpawner>().SpawnEffect();
            WhoParryEntity.AddOrGet<ParryCompleteComponent>().WhomParryEntity = WhomParryEntity;
            WhoParryEntity.AddOrGet<ParryCompleteComponent>().WhoParryEntity = WhoParryEntity;
            
            ServiceLocator.For(WhomParryEntity).Get<CameraShakeService>().Shake(1, 0.5f);
            Animator animator = WhoParryEntity.Get<AnimationHandler>().Animator;
            
            if (WhomParryEntity.TryGet(out StaminaModel staminaModel))
            {
                staminaModel.UpdateStamina(-currentAttackDataInfo.LossStaminaAfterParry);
            }
            
            animator.SetTrigger(Parry);
            animator.SetFloat(ParryIndex, (float) WhomParryEntity.Get<CurrentAttackData>().CurrentParryAnimationIndex);

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