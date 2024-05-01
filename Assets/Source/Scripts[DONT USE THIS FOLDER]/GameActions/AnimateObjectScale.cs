using System;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class AnimateObjectScale : IGameAction
    {
        [SerializeField] private Transform _animatedTransform;
        [SerializeField] private Vector3 _desiredScale;
        [SerializeField] private float _time;

        private TaskStatus _desiredStatus;

        public void OnStart()
        {
            _desiredStatus = TaskStatus.Success;
            _animatedTransform.DOKill();
            _animatedTransform.DOScale(_desiredScale, _time).OnComplete(() => _desiredStatus = TaskStatus.Success);
        }

        public TaskStatus ExecuteAction()
        {
            return _desiredStatus;
        }
    }
}