using UnityEngine;

namespace Source.Modules.InteractionModule.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class InteractionFinderCollider : MonoBehaviour
    {
        [field: SerializeField] public Collider Collider { get; private set; }
    }
}