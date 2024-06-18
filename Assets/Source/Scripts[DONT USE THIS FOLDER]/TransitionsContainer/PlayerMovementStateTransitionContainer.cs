using System.Collections.Generic;
using Sirenix.Serialization;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts.Transitions;
using States;
using Transitions;
using UnityEngine;

namespace Source.Scripts.TransitionsContainer
{
    public class PlayerMovementStateTransitionContainer : TransitionContainer
    {
        [SerializeField] private InputKeyDownTransition _inputKeyDownTransition;
        [SerializeField] private MouseDownTransition _mouseDownTransition;
        [SerializeField] private MouseDownTransition _desiredToProtection;
        [SerializeField] private CurrentHealthLessTransition _lessTransition;
        [OdinSerialize] private CanInteractWithInteraction _canInteractWithInteraction;

        public override List<Transition> GetTransitions()
        {
            Transitions ??= new List<Transition>()
            {
                _inputKeyDownTransition,
                _mouseDownTransition,
                _desiredToProtection,
                _lessTransition,
                _canInteractWithInteraction
            };
            return Transitions;
        }
    }
}