using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Modules.MoveSetModule.Scripts
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
}