using System;
using UniRx;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class InputMouseDownAndAttackEndTransition : Transition
    {
        [SerializeField] private int _mouseIndex;
        [SerializeField] private AttackEventListener _attackEventListener;

        private IDisposable _disposable;
        private bool _attackEnded;

        public override void OnEnable()
        {
            _attackEnded = false;
            _attackEventListener.AttackEnded += OnAttackEnded;
            _disposable = Observable.EveryUpdate().Where(x => _attackEnded && Input.GetMouseButtonDown(_mouseIndex)).Take(1)
                .Subscribe(_ => { OnNeedTransit(this); });
        }

        public override void OnDisable()
        {
            _attackEventListener.AttackEnded -= OnAttackEnded;
            _disposable?.Dispose();
        }

        private void OnAttackEnded()
        {
            _attackEnded = true;
        }
    }
}