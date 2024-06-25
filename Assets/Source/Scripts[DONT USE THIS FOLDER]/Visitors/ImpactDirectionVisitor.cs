using Interfaces;
using Source.Modules.HealthModule.Scripts;
using UnityEngine;
using Animation = Source.Scripts.Enemy.Animation;

namespace Source.Scripts.Visitors
{
    public class ImpactDirectionVisitor : IVisitor
    {
        private readonly Vector2 _direction;

        public ImpactDirectionVisitor(Vector2 direction)
        {
            _direction = direction;
        }

        public void Visit(HealthComponent healthComponent)
        {
          
        }

        public void Visit(Animation animation)
        {
            animation.SetImpactDirection(_direction);
        }
    }
}