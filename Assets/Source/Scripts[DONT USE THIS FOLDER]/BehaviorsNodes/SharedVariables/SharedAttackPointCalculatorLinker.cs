using System;
using BehaviorDesigner.Runtime;
using Source.Scripts.InterfaceLinker;

namespace Source.Scripts.BehaviorsNodes.SharedVariables
{
    [Serializable]
    public class SharedAttackPointCalculatorLinker : SharedVariable<AttackPointCalculatorLinker>
    {
        public static implicit operator SharedAttackPointCalculatorLinker(AttackPointCalculatorLinker value) => new SharedAttackPointCalculatorLinker { Value = value };
    }
}