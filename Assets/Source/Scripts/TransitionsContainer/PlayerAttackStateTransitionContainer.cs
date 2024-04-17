using System.Collections.Generic;
using States;
using Transitions;
using UnityEngine;

namespace TransitionsContainer
{
    public class PlayerAttackStateTransitionContainer : TransitionContainer
    {
        [SerializeField] private InputKeyDownAndAttackEndTransition _inputKeyDownAndAttackEndTransition;
        [SerializeField] private AttackAnimationEndTransition attackAnimationEndTransition;

        public override List<Transition> GetTransitions()
        {
            Transitions ??= new List<Transition>()
            {
                _inputKeyDownAndAttackEndTransition,
                attackAnimationEndTransition
            };
            return Transitions;
        }
    }
}