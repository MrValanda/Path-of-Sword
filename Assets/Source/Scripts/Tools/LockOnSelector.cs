using System;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tools
{
    public class LockOnSelector : OptimizedMonoBehavior
    {
        [SerializeField] private CinemachineVirtualCamera _cameraLockOn;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _radiusLockAt;

        public bool IsLocked { get; private set; }
        public LockOnTarget CurrentLockOnTarget { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (IsLocked == false)
                {
                    TrySelectLockOnTarget();
                }
                else
                {
                    ResetLockOn();
                }
            }
        }

        private void TrySelectLockOnTarget()
        {
            Collider[] overlapSphere = Physics.OverlapSphere(transform.position, _radiusLockAt, _layerMask,
                QueryTriggerInteraction.Collide);

            LockOnTarget closestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (Collider collider in overlapSphere)
            {
                LockOnTarget lockOnTarget = collider.GetComponent<LockOnTarget>();
                if (lockOnTarget != null)
                {
                    float distance = Vector3.Distance(transform.position, lockOnTarget.CameraTarget.position);
                    if (distance < closestDistance)
                    {
                        closestTarget = lockOnTarget;
                        closestDistance = distance;
                    }
                }
            }

            if (closestTarget != null && closestTarget.Equals(CurrentLockOnTarget) == false)
            {
                _cameraLockOn.Priority = 11;
                _cameraLockOn.LookAt = closestTarget.CameraTarget;
                closestTarget.Deactivated += OnTargetDeactivated;
                IsLocked = true;
            }
            else
            {
                ResetLockOn();
            }

            CurrentLockOnTarget = closestTarget;
        }

        private void OnTargetDeactivated()
        {
            if (CurrentLockOnTarget != null)
            {
                CurrentLockOnTarget.Deactivated -= OnTargetDeactivated;
            }

            TrySelectLockOnTarget();
        }

        private void ResetLockOn()
        {
            IsLocked = false;
            _cameraLockOn.Priority = 0;
            CurrentLockOnTarget = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _radiusLockAt);
        }
    }
}