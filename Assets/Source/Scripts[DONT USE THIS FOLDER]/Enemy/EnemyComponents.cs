using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Source.Scripts.Enemy
{
    [Serializable]
    public sealed class EnemyComponents
    {
        [field: SerializeField] public BehaviorTree BehaviorTree { get; private set; }
        [field: SerializeField] public Animation Animation { get; private set; }
        [field: SerializeField] public EnemyHitbox Hitbox { get; private set; }
        [field: SerializeField] public EnemyMovement NpcMovement { get; private set; }
        [field: SerializeField] public Transform WeaponParentLeftHand { get; private set; }
        [field: SerializeField] public Transform WeaponParentRightHand { get; private set; }
        
    }
}