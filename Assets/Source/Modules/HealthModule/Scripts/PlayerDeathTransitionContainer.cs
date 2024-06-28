using System.Collections.Generic;
using States;
using UnityEngine;

namespace Source.Modules.HealthModule.Scripts
{
    public class PlayerDeathTransitionContainer : TransitionContainer
    {
        [SerializeField] private CurrentHealthMoreTransition _currentHealthMoreTransition;
        public override List<Transition> GetTransitions()
        {
            return new List<Transition>() { _currentHealthMoreTransition };
        }
    }
}