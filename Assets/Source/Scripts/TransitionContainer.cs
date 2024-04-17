using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public abstract class TransitionContainer : MonoBehaviour
    {
        protected List<Transition> Transitions;
        public abstract List<Transition> GetTransitions();
    }
}