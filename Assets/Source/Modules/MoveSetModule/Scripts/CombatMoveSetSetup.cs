using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Scripts.CombatModule
{
    [CreateAssetMenu(fileName = nameof(CombatMoveSetSetup), menuName = "Setups/" + nameof(CombatMoveSetSetup),
        order = 0)]
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
        [field: SerializeField] public int NumberOfHitsPerUnit { get; private set; }
        [field: SerializeField, Min(0)] public float DelayBetweenHits { get; private set; }

#if UNITY_EDITOR
        [Button]
        public void AutomaticsCalculateDelay()
        {
            AnimationEvent startAttackEvent =
                AnimationClip.events.FirstOrDefault(x => x.functionName.Equals("StartAttack"));
            AnimationEvent endAttackEvent =
                AnimationClip.events.FirstOrDefault(x => x.functionName.Equals("EndAttack"));

            if(startAttackEvent == null || endAttackEvent == null) return;
            
            float attackTime = endAttackEvent.time - startAttackEvent.time;
            DelayBetweenHits = attackTime / NumberOfHitsPerUnit;
        }
#endif
    }
}