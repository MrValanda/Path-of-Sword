using System;
using Source.Modules.MeshTrailModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Modules.AbilityModule.Scripts
{
    [Serializable]
    public class StartSpawnMeshTrail : IAbilityAction
    {
        [SerializeField] private MeshTrailView _meshTrailView;
        [SerializeField] private float _timeToDestroyTrailView;
        [SerializeField] private float _intervalToSpawnTrailView;
        [SerializeField] private Material _trailMaterial;
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = abilityCaster.Get<SkinnedMeshRenderer>();

            MeshTrailSpawner meshTrailSpawner = new (new MeshTrailViewSpawnData(_meshTrailView, skinnedMeshRenderer.transform),
                _timeToDestroyTrailView, _intervalToSpawnTrailView);
            abilityCaster.Add(meshTrailSpawner);
            meshTrailSpawner.StartSpawn(skinnedMeshRenderer, _trailMaterial);
        }
    }
}