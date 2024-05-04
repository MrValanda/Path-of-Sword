using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace SkinLogic
{
    public class RootLinker : OptimizedMonoBehavior
    {
        private TransformAccessArray _baseRoot;
        private TransformAccessArray _linkedRoot;

        public void Initialize(Transform[] baseRoot, Transform[] linkedRoot)
        {
            _baseRoot.Dispose();
            _linkedRoot.Dispose();
            _baseRoot = new TransformAccessArray(baseRoot);
            _linkedRoot = new TransformAccessArray(linkedRoot);
        }

        private void OnDestroy()
        {
            _baseRoot.Dispose();
            _linkedRoot.Dispose();
        }

        private void Update()
        {
            RootLinkerJob rootLinkerJob = new RootLinkerJob(){LinkedRoot = _linkedRoot};

            JobHandle linkHandle = rootLinkerJob.Schedule(_baseRoot);
            linkHandle.Complete();
        }
    }

    public struct RootLinkerJob : IJobParallelForTransform
    {
        public TransformAccessArray LinkedRoot;
        public void Execute(int index, TransformAccess transform)
        {
            transform.position = LinkedRoot[index].position;
            transform.rotation = LinkedRoot[index].rotation;
        }
    }
}