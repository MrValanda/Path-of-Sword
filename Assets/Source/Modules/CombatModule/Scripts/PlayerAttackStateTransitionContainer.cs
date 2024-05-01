using System.Collections.Generic;
using States;
using Transitions;
using UnityEngine;
using UnityEngine.Serialization;

namespace TransitionsContainer
{
    public class PlayerAttackStateTransitionContainer : TransitionContainer
    {
        [FormerlySerializedAs("_inputKeyDownAndAttackEndTransition")] [SerializeField] private InputKeyPressedAndAttackEndTransition inputKeyPressedAndAttackEndTransition;
        [SerializeField] private AttackAnimationEndTransition attackAnimationEndTransition;
        [SerializeField] private InputMouseDownAndAttackEndTransition _inputMouseDownAndAttackEndTransition;

        public override List<Transition> GetTransitions()
        {
            Transitions ??= new List<Transition>()
            {
                inputKeyPressedAndAttackEndTransition,
                attackAnimationEndTransition,
                _inputMouseDownAndAttackEndTransition
            };
            return Transitions;
        }
    }
}