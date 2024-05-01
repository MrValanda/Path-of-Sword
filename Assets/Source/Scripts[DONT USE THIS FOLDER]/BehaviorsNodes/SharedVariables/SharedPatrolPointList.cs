using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Source.Scripts.Enemy;

namespace Source.Scripts.BehaviorsNodes.SharedVariables
{
    [Serializable]
    public class SharedPatrolPointList : SharedVariable<List<PatrolPointData>>
    {
        public static implicit operator SharedPatrolPointList(List<PatrolPointData> value) => new SharedPatrolPointList { Value = value };
    }
}