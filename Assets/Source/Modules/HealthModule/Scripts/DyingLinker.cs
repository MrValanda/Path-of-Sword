using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Source.Scripts.Interfaces;

namespace Source.Scripts.InterfaceLinker
{
    public class DyingLinker : SerializedMonoBehaviour
    {
        [field: OdinSerialize] public IDying Value { get; private set; }
    }
}