using System;
using Sirenix.Utilities;
using Source.Modules.SkinLogicModule.Scripts;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace SkinLogic
{
    public class RootLinker : OptimizedMonoBehavior
    {
        private TransformAccessArray _baseRoot;
        private TransformAccessArray _linkedRoot;
        private NativeArray<Vector3> _linkedRootPositions;
        private NativeArray<Quaternion> _linkedRootRotations;

        public void Initialize(Transform[] baseRoot, Transform[] linkedRoot)
        {
            if (_baseRoot.isCreated)
            {
                _baseRoot.Dispose();
            }

            if (_linkedRoot.isCreated)
            {
                _linkedRoot.Dispose();
                _linkedRootPositions.Dispose();
                _linkedRootRotations.Dispose();
            }

            _baseRoot = new TransformAccessArray(baseRoot);
            _linkedRoot = new TransformAccessArray(linkedRoot);
            _linkedRootPositions = new NativeArray<Vector3>(linkedRoot.Length, Allocator.Persistent);
            _linkedRootRotations = new NativeArray<Quaternion>(linkedRoot.Length, Allocator.Persistent);
        }

        private void OnDestroy()
        {
            _baseRoot.Dispose();
            _linkedRoot.Dispose();
        }

        private void Update()
        {
            TransformsConverterToValueTypes linkedRootTransformsToValueTypes = new TransformsConverterToValueTypes()
            {
                Positions = _linkedRootPositions,
                Rotations = _linkedRootRotations
            };
            
            JobHandle converterHandle = linkedRootTransformsToValueTypes.Schedule(_linkedRoot);
            converterHandle.Complete();
            
            RootLinkerJob rootLinkerJob = new RootLinkerJob()
            {
                Positions = _linkedRootPositions,
                Rotations = _linkedRootRotations
            };
            
            JobHandle linkHandle = rootLinkerJob.Schedule(_baseRoot);
            linkHandle.Complete();
        }
    }
}