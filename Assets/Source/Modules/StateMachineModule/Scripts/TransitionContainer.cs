using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace States
{
    public abstract class TransitionContainer : SerializedMonoBehaviour
    {
        protected List<Transition> Transitions;
        public abstract List<Transition> GetTransitions();
    }
}