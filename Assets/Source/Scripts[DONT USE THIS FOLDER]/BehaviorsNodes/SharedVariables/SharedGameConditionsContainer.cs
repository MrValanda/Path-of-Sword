using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Source.Scripts.Interfaces;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.BehaviorsNodes.SharedVariables
{
    [Serializable]
    public class SharedGameConditionsContainer : SharedVariable<List<IGameCondition>>
    {
        public static implicit operator SharedGameConditionsContainer(List<IGameCondition> value) => new SharedGameConditionsContainer { Value = value };
    }
}