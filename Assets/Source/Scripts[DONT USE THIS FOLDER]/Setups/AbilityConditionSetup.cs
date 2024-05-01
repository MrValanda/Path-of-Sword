using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Setups
{
    [CreateAssetMenu(fileName = "AbilityConditionSetup", menuName = "Setups/AbilityConditionSetup")]
    public class AbilityConditionSetup : SerializedScriptableObject
    {
        [field: OdinSerialize] public List<IAbilityCondition> AbilityConditions;
    }
}