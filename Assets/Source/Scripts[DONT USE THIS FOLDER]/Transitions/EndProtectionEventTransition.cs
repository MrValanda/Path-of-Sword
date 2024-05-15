using System;
using Source.Modules.CombatModule.Scripts;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class EndProtectionEventTransition : Transition
    {
        [SerializeField] private ProtectionEventListener _protectionEventListener;

        public override void OnEnable()
        {
            _protectionEventListener.ProtectionEnded += OnProtectionEnded;
        }

        public override void OnDisable()
        {
            _protectionEventListener.ProtectionEnded -= OnProtectionEnded;
        }

        private void OnProtectionEnded()
        {
            OnNeedTransit(this);
        }
    }
}