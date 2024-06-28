using System;
using Source.Modules.CombatModule.Scripts;
using Source.Modules.DamageableFindersModule;
using Source.Scripts.EntityLogic;
using Source.Scripts.InterfaceLinker;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.AbilityConditions
{
    [Serializable]
    public class DamageableTargetIsAttacking : IAbilityCondition
    {
        public bool CanExecute(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            DamageableLinker selectedDamageable = abilityCaster.Get<DamageableSelector>().SelectedDamageable;
            if (selectedDamageable == null) return false;

            return selectedDamageable.Owner.Contains<CurrentAttackData>();
        }
    }
}