using System.Collections.Generic;
using Lean.Pool;
using Source.Scripts.AnimationEventListeners;
using Source.Scripts.GameExtensions;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups.Characters;
using UnityEngine;
using UnityEngine.Scripting;

namespace Source.Scripts.Abilities
{
    public class AbilityAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AbilityAnimationEventListener _abilityAnimationEventListener;
        
        private static readonly int UseAbility = Animator.StringToHash("UseAbility");
        private static readonly int AnimationSpeed = Animator.StringToHash("AnimationSpeed");

        private List<IDamageable> _cachedDamagables = new();
        private DamageableContainerSetup _damageableContainerSetup;
        private double _currentDamage;
        
        private void Start()
        {
            _abilityAnimationEventListener.AbilityDestroyEvent += DestroyAnimation;
        }

        public void Init(DamageableContainerSetup damageableContainerSetup, double damage)
        {
            _damageableContainerSetup = damageableContainerSetup;
            _currentDamage = damage;
            _cachedDamagables.Clear();
        }
        
        private void OnDestroy()
        {
            _abilityAnimationEventListener.AbilityDestroyEvent -= DestroyAnimation;
        }

        public void ShowAnimation(float speed)
        {
            _animator.SetFloat(AnimationSpeed, speed);
            _animator.SetTrigger(UseAbility);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && _cachedDamagables.Contains(damageable) == false && other.CanAttackUnit(_damageableContainerSetup))
            {
                _cachedDamagables.Add(damageable);
                
                damageable.ApplyDamage(_currentDamage);
            }
        }
        
        [Preserve]
        private void DestroyAnimation()
        {
            LeanPool.Despawn(this);
        }
    }
}