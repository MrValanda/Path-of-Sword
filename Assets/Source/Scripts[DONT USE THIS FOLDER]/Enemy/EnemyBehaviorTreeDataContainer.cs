using System;
using UnityEngine;

namespace Source.Scripts.Enemy
{
    [Serializable]
    public abstract class EnemyBehaviorTreeDataContainer : MonoBehaviour
    {
        public abstract GameActionContainer GetAttackActionsContainer();
        public abstract GameActionContainer GetStartUseAbilityActionsContainer();
        public abstract GameActionContainer GetProcessingUseAbilityActionsContainer();
        public abstract GameActionContainer GetAfkGameActionsContainer();
        public abstract GameActionContainer GetDeathActionsContainer();
        public abstract GameActionContainer GetTakeDamageGameActionsContainer();
        public abstract GameConditionsContainer GetIsDeathConditionsContainer();
        public abstract GameConditionsContainer GetCanSeeConditionsContainer();
        public abstract GameConditionsContainer GetCanAttackConditionsContainer();
        public abstract GameConditionsContainer GetNeedToBackSpawnPointConditionsContainer();
        public abstract GameConditionsContainer GetCanUseAbilityConditionsContainer();
        public abstract GameConditionsContainer GetAbilityProcessingConditionsContainer();
    }
}