using System;
using Source.Modules.CombatModule.Scripts;
using UniRx;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class DodgeTransition : Transition
    {
        [SerializeField] private KeyCode _keyCode;
        [SerializeField] private DodgeEventListener _dodgeEventListener;
        [SerializeField] private AttackEventListener _attackEventListener;

        private IDisposable _disposable;
        private IDisposable _inputListeningDisposable;
        private bool _canListenKey;
        private bool _inputWasPressed;

        public override void OnEnable()
        {
            _canListenKey = false;
            _inputWasPressed = false;
            _dodgeEventListener.StartListenDodge += OnStartListenDodge;
            _attackEventListener.AttackAnimationEnd += OnAttackEnded;
            _attackEventListener.StartListenCombo += OnStartListenCombo;
            _disposable = Observable.EveryUpdate().Where(x => _canListenKey && _inputWasPressed).Take(1)
                .Subscribe(_ => { OnNeedTransit(this); });
        }

        public override void OnDisable()
        {
            _dodgeEventListener.StartListenDodge -= OnStartListenDodge;
            _attackEventListener.AttackAnimationEnd -= OnAttackEnded;
            _attackEventListener.StartListenCombo -= OnStartListenCombo;
            _disposable?.Dispose();
            _inputListeningDisposable?.Dispose();
        }

        private void OnStartListenCombo()
        {
            _inputListeningDisposable?.Dispose();
            _inputListeningDisposable = Observable.EveryUpdate().Where(_ => Input.GetKey(_keyCode)).Take(1).Subscribe(
                _ => { _inputWasPressed = true; });
        }

        private void OnAttackEnded()
        {
            _canListenKey = false;
        }

        private void OnStartListenDodge()
        {
            _canListenKey = true;
        }
    }
}