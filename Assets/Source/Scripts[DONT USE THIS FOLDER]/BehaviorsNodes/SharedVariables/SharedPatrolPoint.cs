using System;
using BehaviorDesigner.Runtime;
using Source.Scripts.Enemy;

namespace Source.Scripts.BehaviorsNodes.SharedVariables
{
    [Serializable]
    public class SharedPatrolPoint : SharedVariable<PatrolPointData>
    {
        public static implicit operator SharedPatrolPoint(PatrolPointData value) => new SharedPatrolPoint { Value = value };
    }
}