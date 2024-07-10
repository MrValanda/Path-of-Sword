using Source.Modules.Tools;
using UnityEngine;

namespace Source.Modules.DamageableFindersModule
{
    public class DamageableFinderCollider : OptimizedMonoBehavior
    {
        [field: SerializeField] public Collider Collider { get; private set; }
    }
}