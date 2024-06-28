using System;
using Source.Modules.CombatModule.Scripts;
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
            _dodgeEventListener.StartListenDodgeAttack += OnDodgeEnded;
            _disposable = Observable.EveryUpdate().Where(_ => _dodgeEnded && Input.GetMouseButton(_mouseIndex)).Take(1)
                .Subscribe(
                    _ => { OnNeedTransit(this); });
        }

        public override void OnDisable()
        {
            _disposable?.Dispose();
            _dodgeEventListener.StartListenDodgeAttack -= OnDodgeEnded;
        }

        private void OnDodgeEnded()
        {
            _dodgeEnded = true;
        }
    }
}