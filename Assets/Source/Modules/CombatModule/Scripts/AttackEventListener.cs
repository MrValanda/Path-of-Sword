using System;
using Source.Scripts.Tools;
using UnityEngine;
using UnityEngine.Scripting;

namespace Source.Modules.CombatModule.Scripts
{
    public class AttackEventListener : OptimizedMonoBehavior
    {
        public event Action AttackEnded;
        public event Action StartListenCombo;
        public event Action StopListenCombo;
        public event Action AttackAnimationEnd;
        public event Action AttackStarted;

        [SerializeField] private AnimationHandler _animationHandler;
        

        [Preserve]
        private void StartListen()
        {
            StartListenCombo?.Invoke();
        }

        [Preserve]
        private void StartAttack()
        {
            AttackStarted?.Invoke();
        }

        [Preserve]
        private void StopListen()
        {
            if (_animationHandler.IsInTransition)
            {
                return;
            }

            StopListenCombo?.Invoke();
        }

        [Preserve]
        private void EndAttack()
        {
            AttackEnded?.Invoke();
        }

        [Preserve]
        private void AnimationEnd()
        {
            if (_animationHandler.IsInTransition)
            {
                return;
            }

            AttackAnimationEnd?.Invoke();
        }
    }
}