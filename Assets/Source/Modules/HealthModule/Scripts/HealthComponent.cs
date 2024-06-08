using System;
using Interfaces;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
using UnityEngine;

namespace Source.Modules.HealthModule.Scripts
{
    public class HealthComponent : OptimizedMonoBehavior, IVisitable, IDamageable, IDying
    {
        public event Action<IDying> Dead;
        public event Action<double> ReceivedDamage;

        [SerializeField] private Entity _entity;
        [field:SerializeField, Min(0)] public float MaxHealth { get; private set; }

        public Entity Entity => _entity;

        public double CurrentHealth { get; private set; }
        public bool IsDead => CurrentHealth == 0;

        private void Start()
        {
            CurrentHealth = MaxHealth;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                CurrentHealth = MaxHealth;
            }
        }

        public void SetMaxHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
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
            CurrentHealth =
                Math.Clamp(CurrentHealth - damage, 0, MaxHealth);

            Debug.LogError("TAKE DAMAGE" + damage);
            ReceivedDamage?.Invoke(damage);
            OnApplyDamage(damage);
            if (CurrentHealth == 0)
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