using Source.Scripts.VisitableComponents;
using UnityEngine;
using VisitableComponents;

namespace Source.Scripts.PlayerLogic
{
    public class Player : OptimizedMonoBehavior
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private Animator _animator;
        private static readonly int Impact = Animator.StringToHash("Impact");

        private void Start()
        {
            _healthComponent.ReceivedDamage += OnReceivedDamage;
        }

        private void OnDestroy()
        {
            _healthComponent.ReceivedDamage -= OnReceivedDamage;
        }

        private void OnReceivedDamage(double obj)
        {
            _animator.SetTrigger(Impact);
        }
    }
}