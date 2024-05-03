using System;
using Source.Modules.CombatModule.Scripts;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class AttackAnimationEndTransition : Transition
    {
        [SerializeField] private AttackEventListener _attackEventListener;
        public override void OnEnable()
        {
            _attackEventListener.AttackAnimationEnd += OnAttackReset;
        }

        public override void OnDisable()
        {
            _attackEventListener.AttackAnimationEnd -= OnAttackReset;
        }
        private void OnAttackReset()
        {
            OnNeedTransit(this);
        }
    }
}