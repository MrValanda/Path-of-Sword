using System;
using BehaviorDesigner.Runtime;
using Source.Scripts.InterfaceLinker;

namespace Source.Scripts.BehaviorsNodes.SharedVariables
{
    [Serializable]
    public class SharedDamageableLinker : SharedVariable<DamageableLinker>
    {
        public static implicit operator SharedDamageableLinker(DamageableLinker value) => new SharedDamageableLinker { Value = value };
    }
}