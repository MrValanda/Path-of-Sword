using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Tools
{
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public bool IsInTransition => _animator.IsInTransition(0);

        private Queue<CrossFadeData> _crossFadeQueue = new Queue<CrossFadeData>();
        private AnimatorOverrideController _animatorOverrideController;

        private void Start()
        {
            _animatorOverrideController ??= new AnimatorOverrideController(_animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = _animatorOverrideController;
        }


        private void FixedUpdate()
        {
            if (_crossFadeQueue.Count == 0) return;

            CrossFadeData crossFadeData = _crossFadeQueue.Peek();
            if (_animator.IsInTransition(0) == false)
            {
                _crossFadeQueue.Dequeue();
                if (_crossFadeQueue.Count == 0) return;
                crossFadeData = _crossFadeQueue.Peek();
                _animator.CrossFade(crossFadeData.NextState, crossFadeData.TransitionDuration, crossFadeData.Layer, 0);
            }
        }

        public void CrossFade(string nextState, int layer, float transitionDuration)
        {
            if (_crossFadeQueue.Count == 0)
            {
                _animator.CrossFade(nextState, transitionDuration, layer, 0);
            }

            _crossFadeQueue.Enqueue(new CrossFadeData(nextState, layer, transitionDuration));
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