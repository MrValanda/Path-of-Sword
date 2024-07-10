using System;
using Lean.Pool;
using Source.Modules.HealthModule.Scripts;
using Source.Modules.Tools;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Modules.LockOnTargetModule.Scripts
{
    public class LockOnTarget : OptimizedMonoBehavior
    {
        public event Action Deactivated;
        [SerializeField] private HealthComponent _healthComponent;
        [field: SerializeField] public Transform CameraTarget { get; private set; }
        [SerializeField] private LockView _lockView;

        private LockView _spawnedLockView;

        private void OnEnable()
        {
            _healthComponent.Dead += OnDead;
        }

        private void OnDisable()
        {
            UnSelectTarget();
            Deactivated?.Invoke();
            _healthComponent.Dead -= OnDead;
        }

        private void OnDead(IDying obj)
        {
            Deactivated?.Invoke();
        }

        public void SelectTarget()
        {
            _spawnedLockView = LeanPool.Spawn(_lockView, CameraTarget.position,
                Quaternion.identity);
            _spawnedLockView.Initialize(CameraTarget);
        }

        public void UnSelectTarget()
        {
            if (_spawnedLockView == null) return;
            
            LeanPool.Despawn(_spawnedLockView);
            _spawnedLockView = null;
        }
    }
}