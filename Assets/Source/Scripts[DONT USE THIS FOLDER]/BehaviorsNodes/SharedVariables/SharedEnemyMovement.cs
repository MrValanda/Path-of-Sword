using System;
using BehaviorDesigner.Runtime;
using Source.Scripts.Enemy;
using Source.Scripts.Interfaces;

namespace Source.Scripts.BehaviorsNodes.SharedVariables
{
    [Serializable]
    public class SharedEnemyMovement : SharedVariable<IMovement>
    {
        public static implicit operator SharedEnemyMovement(NpcMovement value) => new SharedEnemyMovement { Value = value };
        public static implicit operator SharedEnemyMovement(EnemyMovement value) => new SharedEnemyMovement { Value = value };
    }
}