using System;
using UniRx;
using UnityEngine;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.Transitions
{
    [Serializable]
    public class InputMagnitudeGreaterThanValue : Transition
    {
        [SerializeField] private float _value;
        private IDisposable _disposable;

        public override void OnEnable()
        {
            _disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                Vector2 input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                if (input.sqrMagnitude >= _value * _value)
                {
                    OnNeedTransit(this);
                }
            });
        }

        public override void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}