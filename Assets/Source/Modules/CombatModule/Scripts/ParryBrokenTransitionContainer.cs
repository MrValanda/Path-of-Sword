using System.Collections.Generic;
using States;
using Transitions;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    public class ParryBrokenTransitionContainer : TransitionContainer
    {
        [SerializeField] private DelayTransition _delayTransition;
        
        public override List<Transition> GetTransitions()
        {
            return new List<Transition> {_delayTransition};
        }
    }
}