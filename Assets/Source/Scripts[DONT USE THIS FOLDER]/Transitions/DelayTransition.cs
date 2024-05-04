using System;
using UniRx;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class DelayTransition : Transition
    {
        [SerializeField] private float _delay;
        
        private IDisposable _disposable;
        
        public override void OnEnable()
        {
            _disposable =  Observable.Timer(TimeSpan.FromSeconds(_delay)).Subscribe(_ => OnNeedTransit(this));
        }
        public override void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}