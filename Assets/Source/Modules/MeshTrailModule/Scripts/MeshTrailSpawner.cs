using System;
using DG.Tweening;
using Lean.Pool;
using UniRx;
using UnityEngine;

namespace Source.Modules.MeshTrailModule.Scripts
{
    public class MeshTrailSpawner
    {
        private readonly float _timeToDestroyTrailView;
        private readonly float _intervalToSpawnTrailView;
        private readonly MeshTrailViewSpawnData _meshTrailViewSpawnData;

        private IDisposable _spawnDisposable;

        public MeshTrailSpawner(
            MeshTrailViewSpawnData meshTrailViewSpawnData,
            float timeToDestroyTrailView,
            float intervalToSpawnTrailView)
        {
            _meshTrailViewSpawnData = meshTrailViewSpawnData;
            _timeToDestroyTrailView = timeToDestroyTrailView;
            _intervalToSpawnTrailView = intervalToSpawnTrailView;
        }

        public void StartSpawn(SkinnedMeshRenderer skinnedMeshRenderer,Material trailMaterial)
        {
            StopSpawn();
            _spawnDisposable = Observable.Timer(TimeSpan.FromSeconds(0),TimeSpan.FromSeconds(_intervalToSpawnTrailView)).Subscribe(_ =>
            {
                MeshTrailView meshTrailView = LeanPool.Spawn(_meshTrailViewSpawnData.MeshTrailView,
                    _meshTrailViewSpawnData.Target.position,
                    _meshTrailViewSpawnData.Target.rotation);
                Mesh mesh = new();
                skinnedMeshRenderer.BakeMesh(mesh);
                meshTrailView.Initialize(mesh, trailMaterial);
                Observable.Timer(TimeSpan.FromSeconds(_timeToDestroyTrailView)).Subscribe(_ =>
                {
                    meshTrailView.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
                    {
                        LeanPool.Despawn(meshTrailView);
                    });
                });
            });
        }

        public void StopSpawn()
        {
            _spawnDisposable?.Dispose();
        }
        
    }
}