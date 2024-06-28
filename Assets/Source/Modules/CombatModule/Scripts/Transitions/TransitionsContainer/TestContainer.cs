using System.Collections.Generic;
using Sirenix.Serialization;
using States;

namespace Source.Scripts.TransitionsContainer
{
    public class TestContainer : TransitionContainer
    {
        [OdinSerialize] private List<Transition> _transitions;
        public override List<Transition> GetTransitions()
        {
            return _transitions;
        }
    }
}