using System;
using Source.Modules.DamageableFindersModule;
using Source.Modules.MovementModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.InterfaceLinker;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    [Serializable]
    public class RotateToTargetAction : IAbilityAction
    {
        [SerializeField] private float _rotationSpeed;

        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            DamageableLinker selectedDamageable = abilityCaster.Get<DamageableSelector>().SelectedDamageable;

            if (selectedDamageable == null)
            {
                return;
            }

            abilityCaster.AddOrGet<RotateToTargetComponent>().Initialize(abilityCaster,
                selectedDamageable.transform, _rotationSpeed);
        }
    }
}