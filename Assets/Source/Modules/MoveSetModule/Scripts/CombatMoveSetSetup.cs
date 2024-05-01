using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Scripts.CombatModule
{
    [CreateAssetMenu(fileName = nameof(CombatMoveSetSetup),menuName = "Setups/" + nameof(CombatMoveSetSetup),order = 0)]
    public class CombatMoveSetSetup : SerializedScriptableObject
    {
        [SerializeField] private List<AttackDataInfo> _moveSet;

        public IReadOnlyList<AttackDataInfo> MoveSet => _moveSet.AsReadOnly();
        
        public AttackDataInfo this[int index] => MoveSet[index];
        public int Count => MoveSet.Count;
    }
    
    [Serializable]
    public struct AttackDataInfo
    {
        [field: SerializeField] public AnimationClip AnimationClip { get; private set; }
        [field: SerializeField] public float RootMultiplierBeforeEndAttack { get; private set; }
        [field: SerializeField] public float RootMultiplierAfterEndAttack { get; private set; }
    }
}
