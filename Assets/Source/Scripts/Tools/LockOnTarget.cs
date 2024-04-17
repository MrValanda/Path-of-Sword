using System;
using UnityEngine;

namespace Tools
{
    public class LockOnTarget : OptimizedMonoBehavior
    {
        public event Action Deactivated;
        [field: SerializeField] public Transform CameraTarget { get; private set; }

        private void OnDisable()
        {
            Deactivated?.Invoke();
        }
    }
}