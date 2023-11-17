using System.Collections.Generic;
using States;
using Transitions;
using UnityEngine;

namespace TransitionsContainer
{
    public class PlayerMovementStateTransitionContainer : TransitionContainer
    {
        [SerializeField] private InputKeyDownTransition _inputKeyDownTransition;
        [SerializeField] private MouseDownTransition _mouseDownTransition;


        public override List<Transition> GetTransitions()
        {
            Transitions ??= new List<Transition>()
            {
                _inputKeyDownTransition,
                _mouseDownTransition
            };
            return Transitions;
        }
    }
}