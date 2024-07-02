using System;
using Source.Modules.MeshTrailModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Modules.AbilityModule.Scripts
{
    [Serializable]
    public class StopSpawnMeshTrail : IAbilityAction
    {
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            abilityCaster.Remove<MeshTrailSpawner>();
        }
    }
}