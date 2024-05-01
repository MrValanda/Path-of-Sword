using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Source.Scripts.Interfaces;

namespace Source.Scripts.InterfaceLinker
{
    public class DamageableLinker : SerializedMonoBehaviour
    {
        [field: OdinSerialize] public IDamageable Value { get; private set; }
    }
}