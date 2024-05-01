using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Scripts.EntityDataComponents
{
    public class ParryComponent
    {
        public Entity WhoParryEntity;
        public Entity WhomParryEntity;
        private static readonly int Parry = Animator.StringToHash("Parry");

        public void Execute()
        {
            WhoParryEntity.Get<AnimationHandler>().Animator.SetTrigger(Parry);
        }
    }
}