using System.Collections.Generic;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.BehaviorTreeModule.Modules.MovementModule;
using Source.Modules.CompositeRootModule;
using Source.Modules.DamageableFindersModule;
using Source.Scripts.AttackPointCalculators;
using Source.Scripts.BehaviorTreeEventSenders;
using Source.Scripts.EntityLogic;
using Source.Scripts.GameActions;
using Source.Scripts.GameConditionals;
using Source.Scripts.Interfaces;
using Source.Scripts.NPC.Collector;
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
            NeedStayAfkBehaviorTreeEventSender needStayAfkBehaviorTreeEventSender = new NeedStayAfkBehaviorTreeEventSender(BehaviorTree);
            _entity.Add(needStayAfkBehaviorTreeEventSender);
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
                    new RotateToTarget(_entity, 5), new EnemyAttackGameAction(_entity, needStayAfkBehaviorTreeEventSender),
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