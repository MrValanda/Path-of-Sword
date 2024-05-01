using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class AnimationImpactGameAction : IGameAction
    {
        [SerializeField] private Animator _animator;
        private static readonly int Impact = Animator.StringToHash("Impact");

        public AnimationImpactGameAction(Animator animator)
        {
            _animator = animator;
        }

        public TaskStatus ExecuteAction()
        {
            //Debug.LogError("TakeDamage");
            _animator.SetTrigger(Impact);
            return TaskStatus.Success;
        }
    }
}