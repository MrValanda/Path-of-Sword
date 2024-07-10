using Source.Modules.Tools;
using UnityEngine;

namespace Source.Modules.LockOnTargetModule.Scripts
{
    public class LockView : OptimizedMonoBehavior
    {
        private Camera _camera;
        private Transform _target;

        public void Initialize(Transform target)
        {
            _target = target;
        }

        public void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if(_target == null) return;
            transform.LookAt(_camera.transform);
            transform.position = _target.position;
        }
    }
}