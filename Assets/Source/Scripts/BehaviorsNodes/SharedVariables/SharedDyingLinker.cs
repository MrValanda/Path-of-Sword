using System;
using BehaviorDesigner.Runtime;
using Source.Scripts.InterfaceLinker;

namespace Source.Scripts.BehaviorsNodes.SharedVariables
{
    [Serializable]
    public class SharedDyingLinker : SharedVariable<DyingLinker>
    {
        public static implicit operator SharedDyingLinker(DyingLinker value) => new SharedDyingLinker { Value = value };
    }
}