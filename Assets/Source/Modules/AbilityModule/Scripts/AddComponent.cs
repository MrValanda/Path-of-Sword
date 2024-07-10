using System;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Modules.AbilityModule.Scripts
{
    [Serializable]
    public class AddComponent<T> : IAbilityAction where T: class, new()
    {
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            abilityCaster.AddOrGet<T>();
        }
    }
}