using System;
using Source.Modules.MovementModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    [Serializable]
    public class StopMoveToTargetAction : IAbilityAction
    {
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            abilityCaster.Remove<MoveToTargetComponent>();
        }
    }
}