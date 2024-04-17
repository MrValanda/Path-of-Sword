using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Setups.Characters
{
    [CreateAssetMenu(fileName = "DamageableContainerSetup", menuName = "Setups/Characters/DamageableContainerSetup")]
    public class DamageableContainerSetup : ScriptableObject
    {
        [field: SerializeField] public List<DamageableType> DamageableTypes { get; private set; }
    }
}