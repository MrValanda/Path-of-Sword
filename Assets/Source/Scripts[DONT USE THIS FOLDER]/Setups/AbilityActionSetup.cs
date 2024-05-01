using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Setups
{
    [CreateAssetMenu(fileName = "AbilityActionSetup", menuName = "Setups/AbilityActionSetup")]
    public class AbilityActionSetup : SerializedScriptableObject
    {
        [field: OdinSerialize] public List<IAbilityAction> AbilityActions;
    }
}