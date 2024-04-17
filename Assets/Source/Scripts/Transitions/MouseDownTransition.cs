using System;
using UniRx;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class MouseDownTransition : Transition
    {
        [SerializeField] private int _mouseKey;
        
        private IDisposable _disposable;
        public override void OnEnable()
        {
            _disposable = Observable.EveryUpdate().Where(x => Input.GetMouseButtonDown(_mouseKey)).Take(1).Subscribe(_ =>
            {
                OnNeedTransit(this);
            });
        }

        public override void OnDisable()
        {
            _disposable?.Dispose();
        }
        
    }
}