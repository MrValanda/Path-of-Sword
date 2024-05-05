using Interfaces;
using Source.Scripts.VisitableComponents;
using UnityEngine;
using VisitableComponents;
using Animation = Source.Scripts.Enemy.Animation;

namespace Tools
{
    public class HitBox : MonoBehaviour, IVisitable
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private Animation _animation;

        public void Accept(IVisitor visitor)
        {
            _animation?.Accept(visitor);
            _healthComponent.Accept(visitor);
        }
    }
}
