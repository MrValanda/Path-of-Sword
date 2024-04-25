using System.Collections.Generic;
using States;
using Transitions;
using UnityEngine;

namespace Source.Scripts.TransitionsContainer
{
    public class PlayerDodgeStateTransitionContainer : TransitionContainer
    {
        [SerializeField] private DodgeEndTransition _dodgeEndTransition;
        [SerializeField] private DodgeEndAndMouseDownTransition _dodgeEndAndMouseDownTransition;

        public override List<Transition> GetTransitions()
        {
            Transitions ??= new List<Transition>()
            {
                _dodgeEndTransition, _dodgeEndAndMouseDownTransition
            };
            return Transitions;
        }
    }
}