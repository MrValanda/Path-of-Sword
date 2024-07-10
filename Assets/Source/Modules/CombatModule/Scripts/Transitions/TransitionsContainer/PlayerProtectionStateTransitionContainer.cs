using System.Collections.Generic;
using Sirenix.Serialization;
using Source.Modules.CombatModule.Scripts.Transitions;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts.Transitions;
using States;
using Transitions;
using UnityEngine;

namespace Source.Scripts.TransitionsContainer
{
    public class PlayerProtectionStateTransitionContainer : TransitionContainer
    {
        [SerializeField] private InputMouseUpTransition _inputMouseUpTransition;
        [SerializeField] private MouseDownTransition _mouseDownTransition;
        [SerializeField] private InputKeyDownTransition _inputKeyDownTransition;
        [SerializeField] private InputMouseDownAndNotParryComponent _inputMouseDownAndNotParryComponent;
        [SerializeField] private CurrentHealthLessTransition _lessTransition;
        [SerializeField] private EntityContainsParryBrokenComponent _entityContainsParryBrokenComponent;
        [OdinSerialize] private CanInteractWithInteraction _canInteractWithInteraction;


        public override List<Transition> GetTransitions()
        {
            Transitions ??= new List<Transition>()
            {
                _inputMouseUpTransition, 
                _mouseDownTransition, 
                _inputKeyDownTransition,
                _inputMouseDownAndNotParryComponent,
                _lessTransition,
                _entityContainsParryBrokenComponent,
                _canInteractWithInteraction
            };
            return Transitions;
        }
    }
}