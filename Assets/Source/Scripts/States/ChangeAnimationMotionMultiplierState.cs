using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace States
{
    public class ChangeAnimationMotionMultiplierState : State
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private ApplyRootMotionHandler _applyRootMotionHandler;
        [SerializeField] private float _newMultiplier;
        private float _previousAnimationMotionMultiplier;

        protected override void OnEnter()
        {
            _previousAnimationMotionMultiplier = _applyRootMotionHandler.GetAnimationMotionMultiplier();
            _applyRootMotionHandler.SetAnimationRootMotionMultiplier(_newMultiplier);
        }

        protected override void OnExit()
        {
            _applyRootMotionHandler.SetAnimationRootMotionMultiplier(_previousAnimationMotionMultiplier);
        }
    }
}