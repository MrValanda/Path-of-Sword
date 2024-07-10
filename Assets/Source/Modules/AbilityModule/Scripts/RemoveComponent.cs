using System;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Modules.AbilityModule.Scripts
{
    [Serializable]
    public class RemoveComponent<T> : IAbilityAction where T: class
    {
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            abilityCaster.Remove<T>();
        }
    }
}