using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Source.Modules.SkinLogicModule.Scripts
{
    public struct RootLinkerJob : IJobParallelForTransform
    {
        [ReadOnly]
        public NativeArray<Vector3> Positions;
        [ReadOnly]
        public NativeArray<Quaternion> Rotations;
        public void Execute(int index, TransformAccess transform)
        {
            transform.position = Positions[index];
            transform.rotation = Rotations[index];
        }
    }
}