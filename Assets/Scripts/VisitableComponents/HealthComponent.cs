using System;
using Interfaces;
using UnityEngine;

namespace VisitableComponents
{
    public class HealthComponent : OptimizedMonoBehavior, IVisitable
    {
        [SerializeField, Min(0)] private float _maxHeath;

        public event Action<float> ReceivedDamage; 
        
        private float _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHeath;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void ApplyDamage(float damage)
        {
            if (damage < 0)
            {
                throw new ArgumentException("Damage less than zero");
            }
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHeath);
            ReceivedDamage?.Invoke(damage);
            if (_currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}