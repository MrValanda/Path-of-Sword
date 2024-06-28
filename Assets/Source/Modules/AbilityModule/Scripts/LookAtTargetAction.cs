using System;
using Source.Modules.DamageableFindersModule;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    [Serializable]
    public class LookAtTargetAction : IAbilityAction
    {
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            if (abilityCaster.Get<DamageableSelector>().SelectedDamageable == null)
            {
                return;
            }

            abilityCaster.transform.forward =
                abilityCaster.Get<DamageableSelector>().SelectedDamageable.transform.position -
                abilityCaster.transform.position;
        }
    }
}