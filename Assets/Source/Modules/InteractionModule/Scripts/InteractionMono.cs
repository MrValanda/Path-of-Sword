using Source.Scripts.EntityLogic;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
using UnityEngine;

namespace Source.Modules.InteractionModule.Scripts
{
    public abstract class InteractionMono : OptimizedMonoBehavior
    {
        [field: SerializeField] public Entity InteractionEntity { get; private set; }

        public abstract void Interact(Entity interactSender);

        public abstract bool CanInteract(Entity entity);

    }
}