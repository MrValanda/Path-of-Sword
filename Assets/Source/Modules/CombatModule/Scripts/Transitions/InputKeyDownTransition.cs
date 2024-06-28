using System;
using UniRx;
using UnityEngine;

namespace Source.Scripts.Transitions
{
    [Serializable]
    public class InputKeyDownTransition : Transition
    {
        [SerializeField] private KeyCode _keyCode;

        private IDisposable _disposable;

        public override void OnEnable()
        {
            _disposable = Observable.EveryUpdate().Where(x => Input.GetKeyDown(_keyCode)).Take(1).Subscribe(_ =>
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