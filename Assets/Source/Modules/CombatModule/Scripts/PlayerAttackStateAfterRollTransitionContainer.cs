using System.Collections.Generic;
using Sirenix.Serialization;
using States;
using Transitions;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    public class PlayerAttackStateAfterRollTransitionContainer : TransitionContainer
    {
        [OdinSerialize] private MoveToNextMoveSetTransition _moveToNextMoveSetTransition;
        [SerializeField] private AttackAnimationEndTransition _attackAnimationEndTransition;
        [SerializeField] private DodgeTransition _dodgeTransition;
        public override List<Transition> GetTransitions()
        {
            return new List<Transition>()
            { 
                _moveToNextMoveSetTransition,
                _attackAnimationEndTransition,
                _dodgeTransition 
            };
        }
    }
}