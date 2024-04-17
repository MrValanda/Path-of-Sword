using System;
using States;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class DodgeEndTransition : Transition
    {
        [SerializeField] private DodgeEventListener _dodgeEventListener;

        public override void OnEnable()
        {
            _dodgeEventListener.DodgeEnded += OnDodgeEnded;
        }

        public override void OnDisable()
        {
            _dodgeEventListener.DodgeEnded -= OnDodgeEnded;
        }

        private void OnDodgeEnded()
        {
            OnNeedTransit(this);
        }
    }
}