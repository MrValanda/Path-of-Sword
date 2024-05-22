using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
using UnityEngine;

namespace Source.Modules.DamageableFindersModule
{
    public class DamageableFinderCollider : OptimizedMonoBehavior
    {
        [field: SerializeField] public Collider Collider { get; private set; }
    }
}