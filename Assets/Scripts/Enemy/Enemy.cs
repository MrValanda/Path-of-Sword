using System;
using DG.Tweening;
using UnityEngine;
using VisitableComponents;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private HealthComponent _healthComponent;
        
        private static readonly int Impact = Animator.StringToHash("Impact");

        private void OnEnable()
        {
            _healthComponent.ReceivedDamage += OnReceivedDamage;
        }

        private void OnDisable()
        {
            _healthComponent.ReceivedDamage -= OnReceivedDamage;
        }

        private void OnReceivedDamage(float obj)
        {
            _animator.SetTrigger(Impact);
            Time.timeScale = 0.3f;
            DOVirtual.DelayedCall(0.3f, () =>
            {
                Time.timeScale = 1f;
            });
        }
    }
}
