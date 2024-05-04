using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace SkinLogic
{
    public struct TransformsConverterToValueTypes : IJobParallelForTransform
    {
        [WriteOnly]
        public NativeArray<Vector3> Positions;
        [WriteOnly]
        public NativeArray<Quaternion> Rotations;
     
        public void Execute(int index, TransformAccess transform)
        {
            Positions[index] = transform.position;
            Rotations[index] = transform.rotation;
        }
    }
}