using System.Collections.Generic;
using Source.Modules.HealthModule.Scripts;
using States;
using Transitions;
using UnityEngine;

namespace Source.Scripts.TransitionsContainer
{
    public class PlayerDodgeStateTransitionContainer : TransitionContainer
    {
        [SerializeField] private DodgeEndTransition _dodgeEndTransition;
        [SerializeField] private DodgeEndAndMouseDownTransition _dodgeEndAndMouseDownTransition;
        [SerializeField] private CurrentHealthLessTransition _lessTransition;

        public override List<Transition> GetTransitions()
        {
            Transitions ??= new List<Transition>()
            {
                _dodgeEndTransition, 
                _dodgeEndAndMouseDownTransition,
                _lessTransition
            };
            return Transitions;
        }
    }
}