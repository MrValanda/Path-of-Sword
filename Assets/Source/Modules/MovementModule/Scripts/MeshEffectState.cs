using Source.Modules.MeshTrailModule.Scripts;
using Source.Scripts;
using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class MeshEffectState : State
    {
        [SerializeField] private SkinnedMeshRenderer _renderer;
        [SerializeField] private Material _material;
        [SerializeField] private MeshTrailView _meshTrailView;
        [SerializeField] private float _timeToDestroyTrailView;
        [SerializeField] private float _intervalSpawnTrailView;

        private MeshTrailSpawner _meshTrailSpawner;
        
        protected override void OnEnter()
        {
            _meshTrailSpawner ??= new MeshTrailSpawner(new MeshTrailViewSpawnData(_meshTrailView, _renderer.transform),
                _timeToDestroyTrailView, _intervalSpawnTrailView);
            _meshTrailSpawner.StartSpawn(_renderer, _material);
        }

        protected override void OnExit()
        {
            _meshTrailSpawner.StopSpawn();
        }
    }
}