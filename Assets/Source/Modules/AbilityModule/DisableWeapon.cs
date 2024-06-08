using System;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    [Serializable]
    public class DisableWeapon : IAbilityAction
    {
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            abilityCaster.Get<Weapon>().Disable();
        }
    }
}