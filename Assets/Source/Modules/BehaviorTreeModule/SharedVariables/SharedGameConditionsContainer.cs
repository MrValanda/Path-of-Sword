using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;

namespace Source.Modules.BehaviorTreeModule.SharedVariables
{
    [Serializable]
    public class SharedGameConditionsContainer : SharedVariable<List<IGameCondition>>
    {
        public static implicit operator SharedGameConditionsContainer(List<IGameCondition> value) => new SharedGameConditionsContainer { Value = value };
    }
}