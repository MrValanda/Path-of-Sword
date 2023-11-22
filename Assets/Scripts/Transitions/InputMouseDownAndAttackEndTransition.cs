using System;
using UniRx;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class InputMouseDownAndAttackEndTransition : Transition
    {
        [SerializeField] private int _mouseKey;
        [SerializeField] private AttackEventListener _attackEventListener;

        private IDisposable _disposable;
        private bool _attackEnded;

        public override void OnEnable()
        {
            _attackEnded = false;
            _attackEventListener.AttackEnded += OnAttackEnded;
            _attackEventListener.AttackStarted += OnAttackStarted;
            _disposable = Observable.EveryUpdate().Where(x => _attackEnded && Input.GetMouseButtonDown(_mouseKey))
                .Take(1)
                .Subscribe(_ => { OnNeedTransit(this); });
        }

        public override void OnDisable()
        {
            _attackEventListener.AttackStarted -= OnAttackStarted;
            _attackEventListener.AttackEnded -= OnAttackEnded;
            _disposable?.Dispose();
        }

        private void OnAttackStarted()
        {
            _attackEnded = false;
        }

        private void OnAttackEnded()
        {
            _attackEnded = true;
        }
    }
}