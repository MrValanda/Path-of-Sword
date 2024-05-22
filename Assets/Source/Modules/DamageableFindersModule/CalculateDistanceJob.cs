using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Source.Scripts.CannonSystem
{
    [BurstCompile]
    public struct CalculateDistanceJob : IJobParallelForTransform
    {
        public NativeArray<float> Distances;
        public Vector3 FromPosition;

        public void Execute(int index, TransformAccess transform)
        {
            float distance = (transform.position - FromPosition).sqrMagnitude;
            Distances[index] = distance;
        }
    }
}