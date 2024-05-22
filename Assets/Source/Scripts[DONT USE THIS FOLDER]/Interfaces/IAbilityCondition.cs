using Source.Scripts.EntityLogic;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.Interfaces
{
    public interface IAbilityCondition
    {
        public bool CanExecute(Transform castPoint, Entity abilityCaster,AbilityDataSetup baseAbilitySetup);
    }
}