using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.GameActions
{
    public class CalculatorNavMeshPath
    {
        private const int MaxDistanceToCheckSamplePosition = 21;

        public Vector3 GetDirectionToNextPoint(Vector3 sourcePosition, Vector3 targetPosition)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 enemyPosition = GetSamplePosition(sourcePosition);
            bool canCalculatePath = NavMesh.CalculatePath(enemyPosition, GetSamplePosition(targetPosition),
                NavMesh.AllAreas, path);
            if (canCalculatePath)
            {
                Vector3 nextPoint = path.corners.Skip(1).FirstOrDefault();
                Vector3 directionMove = (nextPoint - enemyPosition);
                
                directionMove.y = 0;
#if UNITY_EDITOR
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
                }
#endif

                return Vector3.ClampMagnitude(directionMove, 1f);
            }
            return Vector3.zero;
        }

        public Vector3 GetSamplePosition(Vector3 sourcePosition)
        {
            NavMesh.SamplePosition(sourcePosition, out NavMeshHit hit, MaxDistanceToCheckSamplePosition,
                NavMesh.AllAreas);
            return hit.position;
        }
    }
}