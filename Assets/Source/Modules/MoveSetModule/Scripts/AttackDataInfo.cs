using System;
using UnityEngine;

namespace Source.Modules.MoveSetModule.Scripts
{
    [Serializable]
    public struct AttackDataInfo
    {
        [field: SerializeField] public AnimationClip AnimationClip { get; private set; }
        [field: SerializeField] public float RootMultiplierBeforeEndAttack { get; private set; }
        [field: SerializeField] public float RootMultiplierAfterEndAttack { get; private set; }
        [field: SerializeField] public HitInfo HitInfo { get; private set; }
        
    }
}