using UnityEngine;

namespace Source.Scripts.Abilities
{
    public class AnimationPartPlayer
    {
        private readonly Animator _animator;
        private static readonly int AnimationSpeed = Animator.StringToHash("AnimationSpeed");

        public AnimationPartPlayer(Animator animator)
        {
            _animator = animator;
        }

        public void AnimatePartAnimation(float desiredTime, float currentTime)
        {
            float speed = 1 / (desiredTime / currentTime);
            _animator.SetFloat(AnimationSpeed, speed);
        }

        public void Reset()
        {
            _animator.SetFloat(AnimationSpeed, 1);
        }
    }
}