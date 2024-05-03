using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Tools
{
    public class AnimationHandler : MonoBehaviour
    {
        [field:SerializeField] public Animator Animator { get; private set; }

        public bool IsInTransition => Animator.IsInTransition(0);

        private readonly Queue<CrossFadeData> _crossFadeQueue = new Queue<CrossFadeData>();
        private AnimatorOverrideController _animatorOverrideController;

        private void Start()
        {
            _animatorOverrideController ??= new AnimatorOverrideController(Animator.runtimeAnimatorController);
            Animator.runtimeAnimatorController = _animatorOverrideController;
        }


        private void FixedUpdate()
        {
            return;
            if (_crossFadeQueue.Count == 0) return;

            CrossFadeData crossFadeData = _crossFadeQueue.Peek();
            Debug.LogError(Animator.IsInTransition(0));
            if (Animator.IsInTransition(0) == false)
            {
                _crossFadeQueue.Dequeue();
                if (_crossFadeQueue.Count == 0) return;
                crossFadeData = _crossFadeQueue.Peek();
                Animator.CrossFadeInFixedTime(crossFadeData.NextState, crossFadeData.TransitionDuration, crossFadeData.Layer, 0,0);
            }
        }

        public void CrossFade(string nextState, int layer, float transitionDuration)
        {
            Animator.InterruptMatchTarget(false);
            Animator.CrossFade(nextState, transitionDuration, layer, 0,0);
        }

        public void OverrideAnimation(string animationName, AnimationClip newAnimation)
        {
            _animatorOverrideController[animationName] = newAnimation;
        }

        private struct CrossFadeData
        {
            public string NextState;
            public int Layer;
            public float TransitionDuration;

            public CrossFadeData(string nextState, int layer, float transitionDuration)
            {
                NextState = nextState;
                Layer = layer;
                TransitionDuration = transitionDuration;
            }
        }
    }
}