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
            _attackEventListener.StopListenCombo += OnAttackEnded;
            _attackEventListener.StartListenCombo += OnAttackStarted;
            _disposable = Observable.EveryLateUpdate().Where(x => _attackEnded && Input.GetMouseButton(_mouseIndex)).Take(1)
                .Subscribe(_ =>
                {
                    _disposable?.Dispose();
                    OnNeedTransit(this);
                });
        }

        public override void OnDisable()
        {
            _attackEventListener.StopListenCombo -= OnAttackEnded;
            _attackEventListener.StartListenCombo -= OnAttackStarted;
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