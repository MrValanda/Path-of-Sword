using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts.Parry
{
    public class ParryHandler 
    {
        private const string ParryAnimationName = "StrongParryAnimation";
        private Entity _entity;
        private ParryHandlerData _parryHandlerData;
        private float _lastParryQuery;

        public void Initialize(Entity entity,ParryHandlerData parryHandlerData)
        {
            _entity = entity;
            _parryHandlerData = parryHandlerData;
        }
        
        public void Parry()
        {
            if (_entity.Contains<ParryCompleteComponent>())
            {
                _lastParryQuery = 0;
                _entity.Remove<ParryCompleteComponent>();
            }
            
            AnimationClip currentParryAnimation = Time.time - _lastParryQuery >= _parryHandlerData.CooldownToStrongParry
                ? _parryHandlerData.StrongParryAnimation
                : _parryHandlerData.WeakParryAnimation;

            
            _entity.Get<AnimationHandler>()
                .OverrideAnimation(ParryAnimationName,currentParryAnimation);
            
            _lastParryQuery = Time.time;
        }
    }
}
