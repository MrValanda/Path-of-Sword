using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Source.Scripts.CannonSystem
{
    [BurstCompile]
    public struct FindNearestUnitJob : IJobFor
    {
        public NativeArray<float> Distances;

        public NativeArray<int> ResultIndex;

        public float MinDistance;
        
        public void Execute(int index)
        {
            float distance = Distances[index];
            if (distance < MinDistance)
            {
                ResultIndex[0] = index;
                MinDistance = distance;
            }
        }
    }
}