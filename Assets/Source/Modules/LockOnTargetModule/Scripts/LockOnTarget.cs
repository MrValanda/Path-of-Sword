using System;
using Lean.Pool;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
using UnityEngine;

namespace Source.Modules.LockOnTargetModule.Scripts
{
    public class LockOnTarget : OptimizedMonoBehavior
    {
        public event Action Deactivated;
        [field: SerializeField] public Transform CameraTarget { get; private set; }
        [SerializeField] private LockView _lockView;

        private LockView _spawnedLockView;

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

        private void OnDisable()
        {
            Deactivated?.Invoke();
        }
    }
}