using UnityEngine;

namespace Source.Scripts.ResourceFolder
{
    public class InCirclePointFinderByRaycast
    {
        public Vector3 FindFreePointInCircle(Vector3 center, float mainRadius, float deadZoneRadius = 1,
            int numSamples = 15,
            float angleStep = 10)
        {
            for (int i = 0; i < numSamples; i++)
            {
                float angle = Random.Range(0f, angleStep * Mathf.PI);
                float x = center.x + Random.Range(deadZoneRadius, mainRadius) * Mathf.Cos(angle);
                float z = center.z + Random.Range(deadZoneRadius, mainRadius) * Mathf.Sin(angle);

                Vector3 samplePoint = new(x, center.y, z);

                Vector3 direction = samplePoint - center;

                if (IsPointClear(samplePoint, center))
                {
                    Debug.DrawRay(center, direction.normalized * direction.magnitude, Color.green, 10f);
                    return samplePoint;
                }

                Debug.DrawRay(center, direction.normalized * direction.magnitude, Color.red, 10f);
            }

            return center;
        }

        private bool IsPointClear(Vector3 point, Vector3 centre)
        {
            Vector3 direction = point - centre;
            return Physics.Raycast(centre, direction, direction.magnitude) == false;
        }
    }
}