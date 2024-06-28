using System.Collections.Generic;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Transitions;
using Source.Scripts.Transitions;
using States;
using Transitions;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts.Protection
{
    public class PlayerEndProtectionTransitionContainer : TransitionContainer
    {
        [SerializeField] private MouseDownTransition _mouseDownTransitionToProtection;
        [SerializeField] private EndProtectionEventTransition _endProtectionEventTransition;
        [SerializeField] private InputKeyDownTransition _inputKeyDownTransition;
        [SerializeField] private MouseDownTransition _mouseDownTransition;
        [SerializeField] private CurrentHealthLessTransition _currentHealthLessTransition;
        [SerializeField] private InputMagnitudeGreaterThanValue _inputMagnitudeGreaterThanValue;
        
        
        public override List<Transition> GetTransitions()
        {
            return new List<Transition>()
            {
                _mouseDownTransitionToProtection,
                _endProtectionEventTransition,
                _inputKeyDownTransition,
                _mouseDownTransition,
                _currentHealthLessTransition,
                _inputMagnitudeGreaterThanValue
            };
        }
    }
}