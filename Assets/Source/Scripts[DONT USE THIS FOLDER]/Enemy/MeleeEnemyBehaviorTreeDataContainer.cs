using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.GameActions;
using Source.Scripts.GameConditionals;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemy
{
    public class MeleeEnemyBehaviorTreeDataContainer : EnemyBehaviorTreeDataContainer
    {
        [SerializeField] private EnemyAttackGameAction _enemyAttackGameAction;
        [SerializeField] private UseSpellGameAction _useSpellGameAction;
        [SerializeField] private ResetImpactTriggerAction _resetImpactTriggerAction;
        [SerializeField] private EnemyDieGameAction _enemyDieGameAction;
        [SerializeField] private AnimationImpactGameAction _animationImpactGameAction;
        [SerializeField] private PushEnemyAwayFromTargetGameAction pushEnemyAwayFromTargetGameAction;
        [SerializeField] private IsDeathCondition _isDeathCondition;
        [SerializeField] private EnemyCanSeeFOVCondition _enemyCanSeeFOVCondition;
        [SerializeField] private EntityCanAttackCondition entityCanAttackCondition;
        [SerializeField] private EnemyIsMaxChaseDistance _enemyIsMaxChaseDistance;
        [SerializeField] private AbilityProcessingCondition _abilityProcessingCondition;
        [SerializeField] private AbilityCasterCanUseAbilityCondition _abilityCasterCanUseAbilityCondition;
        [SerializeField] private DropResourceGameAction _dropResourceGameAction;

        public override GameActionContainer GetAttackActionsContainer()
        {
            return new(new List<IGameAction>() {_enemyAttackGameAction});
        }

        public override GameActionContainer GetStartUseAbilityActionsContainer()
        {
            return new(new List<IGameAction>()
                {_useSpellGameAction, _dropResourceGameAction});
        }

        public override GameActionContainer GetProcessingUseAbilityActionsContainer()
        {
            return new(new List<IGameAction>() { _dropResourceGameAction});
        }

        public override GameActionContainer GetAfkGameActionsContainer()
        {
            return new(new List<IGameAction>() { });
        }

        public override GameActionContainer GetDeathActionsContainer()
        {
            return new(new List<IGameAction>()
                {_enemyDieGameAction, pushEnemyAwayFromTargetGameAction, _dropResourceGameAction});
        }

        public override GameActionContainer GetTakeDamageGameActionsContainer()
        {
            return new(new List<IGameAction>() {_animationImpactGameAction, _dropResourceGameAction, pushEnemyAwayFromTargetGameAction});
        }

        public override GameConditionsContainer GetIsDeathConditionsContainer()
        {
            return new(new List<IGameCondition>() {_isDeathCondition});
        }

        public override GameConditionsContainer GetCanSeeConditionsContainer()
        {
            return new(new List<IGameCondition>() {_enemyCanSeeFOVCondition});
        }

        public override GameConditionsContainer GetCanAttackConditionsContainer()
        {
            return new(new List<IGameCondition>() {entityCanAttackCondition});
        }

        public override GameConditionsContainer GetNeedToBackSpawnPointConditionsContainer()
        {
            return new(new List<IGameCondition>() {_enemyIsMaxChaseDistance});
        }

        public override GameConditionsContainer GetCanUseAbilityConditionsContainer()
        {
            return new(new List<IGameCondition>() {_abilityCasterCanUseAbilityCondition});
        }

        public override GameConditionsContainer GetAbilityProcessingConditionsContainer()
        {
            return new(new List<IGameCondition>() {_abilityProcessingCondition});
        }
    }
}