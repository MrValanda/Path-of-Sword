using System.Collections.Generic;
using Source.Scripts.Transitions;
using States;
using UnityEngine;

namespace Source.Scripts.TransitionsContainer
{
    public class PlayerProtectionStateTransitionContainer : TransitionContainer
    {
        [SerializeField] private InputMouseUpTransition  _inputMouseUpTransition;

        public override List<Transition> GetTransitions()
        {
            Transitions ??= new List<Transition>()
            {
                _inputMouseUpTransition
            };
            return Transitions;
        }
    }
}