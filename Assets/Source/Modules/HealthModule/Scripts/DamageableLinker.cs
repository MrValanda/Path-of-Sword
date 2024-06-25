using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.InterfaceLinker
{
    public class DamageableLinker : SerializedMonoBehaviour
    {
        [field: SerializeField] public Entity Owner { get; private set; }
        [field: OdinSerialize] public IDamageable Value { get; private set; }
    }
}