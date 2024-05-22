using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class ResetImpactTriggerAction : IGameAction
    {
        [SerializeField] private Animator _animator;
        private static readonly int Impact = Animator.StringToHash("Impact");

        public ResetImpactTriggerAction(Animator animator)
        {
            _animator = animator;
        }

        public TaskStatus ExecuteAction()
        {
            _animator.ResetTrigger(Impact);
            return TaskStatus.Success;
        }
    }
}