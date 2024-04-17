using System;
using UniRx;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class DodgeEndAndMouseDownTransition : Transition
    {
        [SerializeField] private DodgeEventListener _dodgeEventListener;
        [SerializeField] private int _mouseIndex;
        private IDisposable _disposable;
        private bool _dodgeEnded;

        public override void OnEnable()
        {
            _dodgeEnded = false;
            _dodgeEventListener.DodgeEnded += OnDodgeEnded;
            _disposable = Observable.EveryUpdate().Where(_ => _dodgeEnded && Input.GetMouseButton(_mouseIndex)).Take(1)
                .Subscribe(
                    _ => { OnNeedTransit(this); });
        }

        public override void OnDisable()
        {
            _disposable?.Dispose();
            _dodgeEventListener.DodgeEnded -= OnDodgeEnded;
        }

        private void OnDodgeEnded()
        {
            _dodgeEnded = true;
        }
    }
}