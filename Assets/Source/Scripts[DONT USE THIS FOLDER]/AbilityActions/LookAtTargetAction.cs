using System;
using DG.Tweening;
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
        [SerializeField] private float _rotationDuration = 0.1f;

        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            abilityCaster.transform.forward =
                abilityCaster.Get<DamageableSelector>().SelectedDamageable.transform.position -
                abilityCaster.transform.position;
        }
    }
}