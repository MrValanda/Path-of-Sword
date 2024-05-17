using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
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
            transform.LookAt(_camera.transform);
            transform.position = _target.position;
        }
    }
}