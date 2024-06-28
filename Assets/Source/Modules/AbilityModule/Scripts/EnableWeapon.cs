using System;
using Source.Modules.CombatModule.Scripts;
using Source.Modules.MoveSetModule.Scripts;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    [Serializable]
    public class EnableWeapon : IAbilityAction
    {
        [SerializeField] private HitInfo _hitInfo;
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            if (abilityCaster.Contains<Weapon>() == false)
            {
                Debug.LogError("Error");
            }
            abilityCaster.Get<Weapon>().Enable(_hitInfo);
            abilityCaster.AddOrGet<CurrentAttackData>().CurrentHitInfo = _hitInfo;
        }
    }
}