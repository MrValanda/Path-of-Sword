using System.Collections.Generic;
using Source.Modules.HealthModule.Scripts;
using States;
using Transitions;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts.Parry
{
    public class ParryBrokenTransitionContainer : TransitionContainer
    {
        [SerializeField] private DelayTransition _delayTransition;
        [SerializeField] private CurrentHealthLessTransition _lessTransition;
        
        public override List<Transition> GetTransitions()
        {
            return new List<Transition> {_delayTransition,_lessTransition};
        }
    }
}