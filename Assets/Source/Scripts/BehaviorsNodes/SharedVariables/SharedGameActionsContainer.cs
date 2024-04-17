using System;
using BehaviorDesigner.Runtime;

namespace Source.Scripts.BehaviorsNodes.SharedVariables
{
    [Serializable]
    public class SharedGameActionsContainer : SharedVariable<GameActionContainer>
    {
        public static implicit operator SharedGameActionsContainer(GameActionContainer value) => new SharedGameActionsContainer { Value = value };
    }
}