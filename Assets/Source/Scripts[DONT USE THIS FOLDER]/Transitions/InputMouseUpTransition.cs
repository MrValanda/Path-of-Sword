using System;
using UniRx;
using UnityEngine;

namespace Source.Scripts.Transitions
{
    [Serializable]
    public class InputMouseUpTransition : Transition
    {
        [SerializeField] private int _mouseIndex;

        private IDisposable _disposable;

        public override void OnEnable()
        {
            _disposable = Observable.EveryUpdate().Where(x => Input.GetMouseButton(_mouseIndex) == false).Take(1).Subscribe(_ =>
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