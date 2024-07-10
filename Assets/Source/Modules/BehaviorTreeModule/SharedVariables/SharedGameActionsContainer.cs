using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;

namespace Source.Modules.BehaviorTreeModule.SharedVariables
{
    [Serializable]
    public class SharedGameActionsContainer : SharedVariable<List<IGameAction>>
    {
        public static implicit operator SharedGameActionsContainer(List<IGameAction> value) => new SharedGameActionsContainer { Value = value };

        public IGameAction this[int index] => Value[index];
        public int Count => Value.Count;
    }
}