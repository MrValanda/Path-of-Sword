using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    public class FieldOfViewChecker
    {
        public bool Check(Transform source, Transform target, LayerMask layerMask, float radius, float angle)
        {
            Vector3 directionToTarget = target.position - source.position;
            if (directionToTarget.magnitude <= radius)
            {
                if (Physics.Raycast(source.position, directionToTarget.normalized,
                        directionToTarget.magnitude, layerMask))
                {
                    return false;
                }

                float angleBetweenTargetAndObserver =
                    Mathf.Acos(Mathf.Clamp(Vector3.Dot(source.forward.normalized, directionToTarget.normalized),-1,1)) * Mathf.Rad2Deg;

                if (Mathf.Abs(angleBetweenTargetAndObserver) <= angle * 0.5f)
                {
                    return true;
                }
            }

            return false;
        }
    }
}