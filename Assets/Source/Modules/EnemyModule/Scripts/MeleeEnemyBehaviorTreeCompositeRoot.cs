using System.Collections.Generic;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.BehaviorTreeModule.Modules.MovementModule;
using Source.Modules.CombatModule.Scripts.Parry;
using Source.Modules.CompositeRootModule;
using Source.Modules.DamageableFindersModule;
using Source.Modules.EnemyModule.Scripts.IGameActions;
using Source.Modules.EnemyModule.Scripts.IGameConditions;
using Source.Modules.HealthModule.Scripts;
using Source.Modules.StaminaModule.Scripts;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Abilities;
using Source.Scripts_DONT_USE_THIS_FOLDER_.BehaviorsNodes.SharedVariables;
using Source.Scripts.AttackPointCalculators;
using Source.Scripts.BehaviorTreeEventSenders;
using Source.Scripts.EntityLogic;
using Source.Scripts.GameActions;
using Source.Scripts.GameConditionals;
using Source.Scripts.Interfaces;
using Source.Scripts.NPC.Collector;
using UnityEngine;
using Animation = Source.Scripts.Enemy.Animation;

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
            
            Animator animator = _entity.Get<Animation>().Animator;
            
            NeedStayAfkBehaviorTreeEventSender needStayAfkBehaviorTreeEventSender = new(BehaviorTree);
            _entity.Add(needStayAfkBehaviorTreeEventSender);

            DisableEntityComponent<ParryComponent> disableParryComponent =
                new DisableEntityComponent<ParryComponent>(_entity);

            ChangeStaggerImpactValue staggerImpactValue = new(_entity, 0.3f);

            InitSequence(SmartEnemyVariables.CanMoveToTargetConditions, SmartEnemyVariables.MoveToTargetActionsSequence,
                new List<IGameCondition>
                {
                    new DamageableSelectorIsNotNull(_entity)
                },
                new List<IGameAction>
                {
                    new MoveToAttackPointAction(meleeAttackPointCalculator, _entity)
                });

            InitSequence(SmartEnemyVariables.CanAttackTarget, SmartEnemyVariables.AttackTargetActionsSequence,
                new List<IGameCondition>
                {
                    new DamageableSelectorIsNotNull(_entity),
                    new EntityCanAttackCondition(_entity, _obstacleLayer)
                },
                new List<IGameAction>
                {
                    staggerImpactValue, disableParryComponent,
                    new RotateToTarget(_entity, 5),
                    new EnemyAttackGameAction(_entity, needStayAfkBehaviorTreeEventSender)
                });

            InitSequence(SmartEnemyVariables.CanUseAbility, SmartEnemyVariables.UseAbilityActionsSequence,
                new List<IGameCondition>
                {
                    new CanUseAbility(_entity.Get<AbilityCaster>(), _entity)
                },
                new List<IGameAction>
                {
                    staggerImpactValue, disableParryComponent,
                    new UseRandomAbilityAction(_entity.Get<AbilityCaster>(), _entity)
                });

            
            BehaviorTree.InitVariable<SharedGameActionsContainer, List<IGameAction>>(
                SmartEnemyVariables.StartAfkGameActions,
                new List<IGameAction>
                {
                    new SendAnimationTrigger(animator,"Protect"),
                    new SetAnimationBool(animator, true, "IsProtection", false),
                    new SetAnimationBool(animator, true, "IsMovement", false)
                });

            BehaviorTree.InitVariable<SharedGameActionsContainer, List<IGameAction>>(SmartEnemyVariables.AfkGameActions,
                new List<IGameAction>
                {
                    new RotateToTarget(_entity, 5f),
                    new DelayedExecutorAction(2f,new List<IGameAction>()
                    {
                        new SetRandomAnimationFloatValue(animator, "InputX", -1, 1),
                        new SetRandomAnimationFloatValue(animator, "InputY", -1, 1)
                    })
                });


            BehaviorTree.InitVariable<SharedGameActionsContainer, List<IGameAction>>(
                SmartEnemyVariables.EndAfkGameActions,
                new List<IGameAction>
                {
                    new SetAnimationBool(animator, false, "IsProtection", false),
                    new SetAnimationBool(animator, false, "IsMovement", false)
                });


            InitSequence(SmartEnemyVariables.IsDIe, SmartEnemyVariables.DieActions,
                new List<IGameCondition>
                {
                    new IsDieCondition(_entity.Get<HealthComponent>())
                },
                new List<IGameAction>
                {
                    new SetAnimationBool(animator, true, "IsDeath"),
                    new WaitAction(-1)
                });


            InitSequence(SmartEnemyVariables.IsStaminaBroken, SmartEnemyVariables.StaminaBrokeActions,
                new List<IGameCondition>
                {
                    new StaminaBroken(_entity.Get<StaminaModel>())
                }
                , new List<IGameAction>
                {
                    staggerImpactValue,
                    disableParryComponent,
                    new StaminaBrokenAction(_entity),
                    new WaitAction(22f)
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