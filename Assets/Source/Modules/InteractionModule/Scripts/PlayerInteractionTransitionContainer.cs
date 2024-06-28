using States;
using Transitions;
using UnityEngine;
using System.Collections.Generic;

namespace Source.Modules.InteractionModule.Scripts
{
    public class PlayerInteractionTransitionContainer : TransitionContainer
    {
        [SerializeField] private DelayTransition _delayTransition;
        
        public override List<Transition> GetTransitions()
        {
            return new List<Transition>() { _delayTransition };
        }
    }
}