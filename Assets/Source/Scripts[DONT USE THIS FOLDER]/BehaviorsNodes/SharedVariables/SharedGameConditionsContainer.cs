using System;
using BehaviorDesigner.Runtime;

namespace Source.Scripts.BehaviorsNodes.SharedVariables
{
    [Serializable]
    public class SharedGameConditionsContainer : SharedVariable<GameConditionsContainer>
    {
        public static implicit operator SharedGameConditionsContainer(GameConditionsContainer value) => new SharedGameConditionsContainer { Value = value };
    }
}