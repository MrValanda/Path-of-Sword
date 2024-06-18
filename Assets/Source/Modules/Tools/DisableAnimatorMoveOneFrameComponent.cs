using Source.Scripts.EntityLogic;

namespace Source.Modules.Tools
{
    public class DisableAnimatorMoveOneFrameComponent : OneFrameComponent<DisableAnimatorMoveOneFrameComponent>
    {
        public DisableAnimatorMoveOneFrameComponent(Entity entity) : base(entity)
        {
        }
    }
}