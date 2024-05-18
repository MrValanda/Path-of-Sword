using Cinemachine;
using Source.Modules.JobsMethodsModule;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
using UnityEngine;

namespace Source.Modules.LockOnTargetModule.Scripts
{
    public class LockOnSelector : OptimizedMonoBehavior
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _radiusLockAt;

        public bool IsLocked { get; private set; }
        public LockOnTarget CurrentLockOnTarget { get; private set; }

        private CinemachineVirtualCamera _cameraLockOn;

        public void Initialize(CinemachineVirtualCamera cameraLockOn)
        {
            _cameraLockOn = cameraLockOn;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C) == false) return;

            if (IsLocked == false)
            {
                TrySelectLockOnTarget();
            }
            else
            {
                ResetLockOn();
            }
        }

        private void TrySelectLockOnTarget()
        {
            LockOnTarget closestTarget = FindClosestTarget();

            if (closestTarget != null && closestTarget.Equals(CurrentLockOnTarget) == false)
            {
                _cameraLockOn.Priority = 11;
                _cameraLockOn.LookAt = closestTarget.CameraTarget;
                closestTarget.Deactivated += OnTargetDeactivated;
            
                RemoveSpawnedView();
                closestTarget.SelectTarget();

                IsLocked = true;
            }
            else
            {
                ResetLockOn();
            }

            CurrentLockOnTarget = closestTarget;
        }

        private LockOnTarget FindClosestTarget()
        {
            ColliderHit[] overlapSphere = JobsRaycast.OverlapSphere(transform.position, _radiusLockAt,
                new QueryParameters(_layerMask, hitTriggers: QueryTriggerInteraction.Collide));

            LockOnTarget closestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (ColliderHit colliderHit in overlapSphere)
            {
                LockOnTarget lockOnTarget = colliderHit.collider.GetComponent<LockOnTarget>();
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

            return closestTarget;
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
            RemoveSpawnedView();
            _cameraLockOn.Priority = 0;
            CurrentLockOnTarget = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _radiusLockAt);
        }

        private void RemoveSpawnedView()
        {
            if (CurrentLockOnTarget == null) return;

            CurrentLockOnTarget.UnSelectTarget();
        }
    }
}