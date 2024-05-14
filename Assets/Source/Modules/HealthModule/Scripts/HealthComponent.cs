using System;
using Interfaces;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
using UnityEngine;

namespace Source.Scripts.VisitableComponents
{
    public class HealthComponent : OptimizedMonoBehavior, IVisitable, IDamageable, IDying
    {
        public event Action<IDying> Dead;
        public event Action<double> ReceivedDamage;

        [SerializeField, Min(0)] private float _maxHeatlh;
        [SerializeField] private Entity _entity;

        public Entity Entity => _entity;

        private double _currentHealth;
        public bool IsDead => _currentHealth == 0;

        private void Start()
        {
            _currentHealth = _maxHeatlh;
        }

        public void SetMaxHealth(float maxHealth)
        {
            _maxHeatlh = maxHealth;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void ApplyDamage(double damage)
        {
            if (damage < 0)
            {
                throw new ArgumentException("Damage less than zero");
            }

            damage -= damage * _entity.AddOrGet<EntityCurrentStatsData>().DamageReducePercent;
            _currentHealth =
                Math.Clamp(_currentHealth - damage, 0, _maxHeatlh);

            Debug.LogError("TAKE DAMAGE" + damage);
            ReceivedDamage?.Invoke(damage);
            OnApplyDamage(damage);
            if (_currentHealth == 0)
            {
                Dead?.Invoke(this);
            }
        }

        protected virtual void OnDead()
        {
        }

        protected virtual void OnApplyDamage(double damage)
        {
        }
    }
}