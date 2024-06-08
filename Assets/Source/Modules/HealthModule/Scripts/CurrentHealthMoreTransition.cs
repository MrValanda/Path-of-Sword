using System;
using Source.Scripts.VisitableComponents;
using UniRx;
using UnityEngine;

namespace Source.Modules.HealthModule.Scripts
{
    [Serializable]
    public class CurrentHealthMoreTransition : Transition
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private float _value;

        private IDisposable _disposable;

        public CurrentHealthMoreTransition(HealthComponent healthComponent, float value)
        {
            _healthComponent = healthComponent;
            _value = value;
        }

        public override void OnEnable()
        {
            _disposable = Observable.EveryFixedUpdate()
                .Where(_ => _healthComponent.CurrentHealth >= _value)
                .Subscribe(_ =>
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