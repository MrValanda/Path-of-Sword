using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.Modules.StaminaModule.Scripts;
using Source.Modules.Tools;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.InteractionModule.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class FinishAttackInteraction : InteractionMono
    {
        private static readonly int FatalDamageTrigger = Animator.StringToHash("FatalDamageTrigger");
        private static readonly int FinalBlowTrigger = Animator.StringToHash("FinalBlowTrigger");
        private static readonly int IsStunned = Animator.StringToHash("IsStunned");


        public override void StopInteract(Entity interactSender)
        {
            LeanPool.Despawn(InteractionEntity);
        }

        public override bool CanInteract(Entity entity)
        {
            return InteractionEntity.Get<StaminaModel>().CurrentStamina <= 0;
        }

        [Button]
        public override void Interact(Entity interactSender)
        {
            interactSender.transform.DOKill(true);
            interactSender.Add(new DisableAnimatorMoveOneFrameComponent(interactSender));
            interactSender.Get<AnimationHandler>().Animator.SetTrigger(FinalBlowTrigger);
            InteractionEntity.transform.forward =
                (interactSender.transform.position - InteractionEntity.transform.position).normalized;
            InteractionEntity.Get<AnimationHandler>().Animator.SetTrigger(FatalDamageTrigger);
            InteractionEntity.Get<AnimationHandler>().Animator.SetBool(IsStunned, false);
            interactSender.transform.position = InteractionPoint.position;
            interactSender.transform.forward = InteractionPoint.forward;
        }
    }
}