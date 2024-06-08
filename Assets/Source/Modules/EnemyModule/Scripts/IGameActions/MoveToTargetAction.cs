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
    public class MoveToTargetAction : IAbilityAction
    {
        [SerializeField] private float _speed;
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            DamageableLinker selectedDamageable = abilityCaster.Get<DamageableSelector>().SelectedDamageable;

            if (selectedDamageable == null)
            {
                return;
            }

            abilityCaster.AddOrGet<MoveToTargetComponent>().Initialize(abilityCaster,
                selectedDamageable.transform, _speed);
        }
    }
}