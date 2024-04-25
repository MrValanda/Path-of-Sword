using Source.Scripts;
using Tools;
using UnityEngine;

namespace States
{
    public class RotateToLockOnTarget : State
    {
        [SerializeField] private Transform _whoWasRotate;
        [SerializeField] private LockOnSelector _lockOnSelector;

        protected override void OnUpdate()
        {
            if (_lockOnSelector.IsLocked && _lockOnSelector.CurrentLockOnTarget != null)
            {
                Vector3 direction = _lockOnSelector.CurrentLockOnTarget.CameraTarget.position - _whoWasRotate.position;
                direction.y = 0;
                _whoWasRotate.forward = direction;
            }
        }
    }
}