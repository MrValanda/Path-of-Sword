using System.Collections.Generic;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Transitions;
using States;
using Transitions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Modules.CombatModule.Scripts
{
    public class PlayerAttackStateTransitionContainer : TransitionContainer
    {
        [FormerlySerializedAs("_inputKeyDownAndAttackEndTransition")] [SerializeField] private DodgeTransition dodgeTransition;
        [SerializeField] private AttackAnimationEndTransition attackAnimationEndTransition;
        [SerializeField] private InputMouseDownAndAttackEndTransition _inputMouseDownAndAttackEndTransition;
        [SerializeField] private EntityContainsParryBrokenComponent _entityContainsParryBrokenComponent;
        [SerializeField] private CurrentHealthLessTransition _lessTransition;
        [SerializeField] private ReceiveDamageTransition _receiveDamageTransition;


        public override List<Transition> GetTransitions()
        {
            Transitions ??= new List<Transition>()
            {
                _lessTransition,
                dodgeTransition,
                attackAnimationEndTransition,
                _inputMouseDownAndAttackEndTransition,
                _entityContainsParryBrokenComponent,
                _receiveDamageTransition
            };
            return Transitions;
        }
    }
}