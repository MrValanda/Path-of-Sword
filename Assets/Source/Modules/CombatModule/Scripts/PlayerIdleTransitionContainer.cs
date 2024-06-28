using System.Collections.Generic;
using Source.Modules.HealthModule.Scripts;
using States;
using Transitions;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    public class PlayerIdleTransitionContainer : TransitionContainer
    {
        [SerializeField] private DelayTransition _delayTransition;
        [SerializeField] private ReceiveDamageTransition _receiveDamageTransition;
        public override List<Transition> GetTransitions()
        {
            return new List<Transition>() { _delayTransition, _receiveDamageTransition };
        }
    }
}