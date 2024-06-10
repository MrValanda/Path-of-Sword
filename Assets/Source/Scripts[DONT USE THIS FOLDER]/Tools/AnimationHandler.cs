using UnityEngine;

namespace Source.Scripts.Tools
{
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField] private AnimatorOverrideController _animatorOverrideController;
        [field:SerializeField] public Animator Animator { get; private set; }

        public bool IsInTransition => Animator.IsInTransition(0);
        
        private void Awake()
        {
            if (_animatorOverrideController == null)
            {
                _animatorOverrideController = new AnimatorOverrideController(Animator.runtimeAnimatorController);
            }
        
            Animator.runtimeAnimatorController = _animatorOverrideController;
        }

        public void OverrideAnimation(string animationName, AnimationClip newAnimation)
        {
            _animatorOverrideController[animationName] = newAnimation;
        }
    }
}