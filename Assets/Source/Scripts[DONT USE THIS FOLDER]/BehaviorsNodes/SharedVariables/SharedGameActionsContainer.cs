using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts;
using Source.Scripts.Interfaces;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.BehaviorsNodes.SharedVariables
{
    [Serializable]
    public class SharedGameActionsContainer : SharedVariable<List<IGameAction>>
    {
        public static implicit operator SharedGameActionsContainer(List<IGameAction> value) => new SharedGameActionsContainer { Value = value };

        public IGameAction this[int index] => Value[index];
        public int Count => Value.Count;
    }
}