using Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemy
{
    public class Animation : MonoBehaviour, IVisitable
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Impact = Animator.StringToHash("Impact");
        private static readonly int ImpactY = Animator.StringToHash("ImpactY");
        private static readonly int ImpactX = Animator.StringToHash("ImpactX");

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorOverrideController _animatorOverrideController;
        [SerializeField] private bool _test;

        public Animator Animator => _animator;

        private void Awake()
        {
            if (_test)
            {
                return;
            }

            if (_animatorOverrideController == null)
            {
                _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            }

            _animator.runtimeAnimatorController = _animatorOverrideController;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void SetTrigger(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }

        public void SetImpactDirection(Vector2 direction)
        {
            _animator.SetFloat(ImpactX, direction.x);
            _animator.SetFloat(ImpactY, direction.y);
        }

        public void OverrideAnimation(string animationName, AnimationClip newAnimation)
        {
            _animatorOverrideController[animationName] = newAnimation;
        }

        public void ResetTriggers()
        {
            _animator.ResetTrigger(Idle);
            _animator.ResetTrigger(Impact);
        }
    }
}