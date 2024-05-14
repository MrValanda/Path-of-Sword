using System;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts.Parry
{
    public class ParryHandler : IDisposable
    {
        private static string parryAnimationName = "StrongParryAnimation";
        private Entity _entity;
        private ParryHandlerData _parryHandlerData;
        private float _lastParryQuery;

        public void Initialize(Entity entity,ParryHandlerData parryHandlerData)
        {
            _entity = entity;
            _parryHandlerData = parryHandlerData;
        }

        public void Dispose()
        {
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

            Debug.LogError(currentParryAnimation.name);
            
            _entity.Get<AnimationHandler>()
                .OverrideAnimation(parryAnimationName,currentParryAnimation);
            
            _lastParryQuery = Time.time;
        }
        /* из parry handler я кидаю запрос на парирование 
           handler.Parry();
           он у себя под капотом добавляет компонент parry с разными интервалами 
           как высчитывается интервал
           если я успешно отпарировал окно большое 
           если же я есть компонет парирования и я делаю запрос еще
           то я уменьшаю окно 
           если я не вызываю запрос на парирование n времени
           окно становится нормальным 
           */
    }
}
