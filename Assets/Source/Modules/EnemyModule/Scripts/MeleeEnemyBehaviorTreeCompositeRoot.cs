using System.Collections.Generic;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.BehaviorTreeModule.Modules.MovementModule;
using Source.Modules.CombatModule.Scripts.Parry;
using Source.Modules.CompositeRootModule;
using Source.Modules.DamageableFindersModule;
using Source.Modules.EnemyModule.Scripts.IGameActions;
using Source.Modules.EnemyModule.Scripts.IGameConditions;
using Source.Scripts.AttackPointCalculators;
using Source.Scripts.BehaviorTreeEventSenders;
using Source.Scripts.EntityLogic;
using Source.Scripts.GameActions;
using Source.Scripts.GameConditionals;
using Source.Scripts.Interfaces;
using Source.Scripts.NPC.Collector;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Abilities;
using Source.Scripts_DONT_USE_THIS_FOLDER_.BehaviorsNodes.SharedVariables;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts
{
    public class MeleeEnemyBehaviorTreeCompositeRoot : BehaviorTreeCompositeRoot
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private LayerMask _obstacleLayer;

        public override void Compose()
        {
            IAttackPointCalculator meleeAttackPointCalculator =
                new MeleeAttackPointCalculator(GetTarget, _entity.transform);

            NeedStayAfkBehaviorTreeEventSender needStayAfkBehaviorTreeEventSender =
                new NeedStayAfkBehaviorTreeEventSender(BehaviorTree);
            _entity.Add(needStayAfkBehaviorTreeEventSender);

            DisableEntityComponent<ParryComponent> disableParryComponent =
                new DisableEntityComponent<ParryComponent>(_entity);

            ChangeStaggerImpactValue staggerImpactValue = new ChangeStaggerImpactValue(_entity, 0.5f);

            InitSequence(SmartEnemyVariables.CanMoveToTargetConditions, SmartEnemyVariables.MoveToTargetActionsSequence,
                new()
                {
                    new DamageableSelectorIsNotNull(_entity)
                },
                new()
                {
                    new MoveToAttackPointAction(meleeAttackPointCalculator, _entity),
                });

            InitSequence(SmartEnemyVariables.CanAttackTarget, SmartEnemyVariables.AttackTargetActionsSequence,
                new()
                {
                    new DamageableSelectorIsNotNull(_entity), new EntityCanAttackCondition(_entity, _obstacleLayer)
                },
                new()
                {
                    staggerImpactValue, disableParryComponent,
                    new RotateToTarget(_entity, 5),
                    new EnemyAttackGameAction(_entity, needStayAfkBehaviorTreeEventSender),
                });

            InitSequence(SmartEnemyVariables.CanUseAbility, SmartEnemyVariables.UseAbilityActionsSequence,
                new()
                {
                    new CanUseAbility(_entity.Get<AbilityCaster>(), _entity)
                },
                new()
                {
                    staggerImpactValue, disableParryComponent,
                    new UseRandomAbilityAction(_entity.Get<AbilityCaster>(), _entity)
                });

            BehaviorTree.InitVariable<SharedGameActionsContainer, List<IGameAction>>(SmartEnemyVariables.AfkGameActions,
                new()
                {
                });

            BehaviorTree.EnableBehavior();
        }

        private void InitSequence(string condition, string action, List<IGameCondition> conditions,
            List<IGameAction> actions)
        {
            BehaviorTree.InitVariable<SharedGameActionsContainer, List<IGameAction>>(action, actions);

            BehaviorTree.InitVariable<SharedGameConditionsContainer, List<IGameCondition>>(condition, conditions);
        }

        private Transform GetTarget()
        {
            return _entity.Get<DamageableSelector>().SelectedDamageable.transform;
        }
    }
}