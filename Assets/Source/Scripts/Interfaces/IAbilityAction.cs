using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.Interfaces
{
    public interface IAbilityAction
    {
        public void ExecuteAction(Transform castPoint, Enemy.Enemy abilityCaster,AbilityDataSetup baseAbilitySetup);
    }
}