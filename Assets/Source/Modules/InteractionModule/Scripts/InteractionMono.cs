using Source.Modules.Tools;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Modules.InteractionModule.Scripts
{
    public abstract class InteractionMono : OptimizedMonoBehavior
    {
        [field: SerializeField] public Entity InteractionEntity { get; private set; }
        [field: SerializeField] public Transform InteractionPoint { get; private set; }

        public abstract void Interact(Entity interactSender);
        public abstract void StopInteract(Entity interactSender);

        public abstract bool CanInteract(Entity entity);

    }
}