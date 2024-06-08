using Source.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.HealthModule.Scripts
{
    public class DeathState : State
    {
        private static readonly int IsDeath = Animator.StringToHash("IsDeath");
        private static readonly int ImpactY = Animator.StringToHash("ImpactY");
        private static readonly int ImpactX = Animator.StringToHash("ImpactX");

        protected override void OnEnter()
        {
            Animator animator = _entity.Get<AnimationHandler>().Animator;
            animator.SetBool(IsDeath, true);
            _entity.transform.forward = new Vector3(animator.GetFloat(ImpactX), 0, animator.GetFloat(ImpactY));
            _entity.Get<CharacterController>().enabled = false;
        }

        protected override void OnExit()
        {
            _entity.Get<AnimationHandler>().Animator.SetBool(IsDeath, false);
            _entity.Get<CharacterController>().enabled = true;
        }
    }
}