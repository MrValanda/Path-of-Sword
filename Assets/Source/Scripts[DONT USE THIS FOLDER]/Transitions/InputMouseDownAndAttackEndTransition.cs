using System;
using Source.Modules.CombatModule.Scripts;
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
            _disposable = Observable.EveryUpdate().Where(x => _attackEnded && Input.GetMouseButton(_mouseIndex)).Take(1)
                .Subscribe(_ =>
                {
                    _disposable?.Dispose();
                    OnNeedTransit(this);
                });
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