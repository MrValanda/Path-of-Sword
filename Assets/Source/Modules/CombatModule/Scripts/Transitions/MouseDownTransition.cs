using System;
using UniRx;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class MouseDownTransition : Transition
    {
        [SerializeField] private int _mouseKey;
        [SerializeField] private bool _needListenedPressed;

        private IDisposable _disposable;

        public override void OnEnable()
        {
            _disposable = Observable.EveryUpdate()
                .Where(x => GetMouseButtonDown()).Take(1).Subscribe(_ => { OnNeedTransit(this); });
        }

        public override void OnDisable()
        {
            _disposable?.Dispose();
        }

        private bool GetMouseButtonDown()
        {
            return _needListenedPressed
                ? Input.GetMouseButton(_mouseKey)
                : Input.GetMouseButtonDown(_mouseKey);
        }
    }
}