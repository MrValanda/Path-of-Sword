using System;
using UnityEngine;

namespace Source.Scripts.AnimationEventListeners
{
    public enum AttackHand
    {
        Left = 0,
        Right = 1,
        Both = 2,
    }
    
    [RequireComponent(typeof(Animator))]
    public class AttackAnimationEventListener : MonoBehaviour
    {
        public event Action<AttackHand> AttackStarted;
        public event Action<AttackHand> AttackEnded;

        private void Enable(int attackHandIndex)
        {
            AttackStarted?.Invoke((AttackHand)attackHandIndex);
        }

        private void Disable(int attackHandIndex)
        {
            AttackEnded?.Invoke((AttackHand)attackHandIndex);
        }
    }
}