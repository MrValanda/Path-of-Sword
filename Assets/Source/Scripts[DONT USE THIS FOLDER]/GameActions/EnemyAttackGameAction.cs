using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.Abilities;
using Source.Scripts.AnimationEventListeners;
using Source.Scripts.BehaviorTreeEventSenders;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using Source.Scripts.Setups.Characters;
using UnityEngine;
using Animation = Source.Scripts.Enemy.Animation;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class EnemyAttackGameAction : IGameAction
    {
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

        private static readonly List<string> AttackStateNames = new()
        {
            "Attack",
            "Attack2",
        };

        private Entity _entity;
        private AbilityEventListener _abilityEventListener;
        private NeedStayAfkBehaviorTreeEventSender _needStayAfkBehaviorTreeEventSender;

        private int _previousIndex;
        private int _previousChainIndex;
        private int _previousAttackStateNameIndex;
        private Ability _currentUsedAttackAbility;
        private Transform _castPoint;
        private bool _needCastNewSpell;

        public EnemyAttackGameAction(Entity entity,
            NeedStayAfkBehaviorTreeEventSender needStayAfkBehaviorTreeEventSender)
        {
            _entity = entity;
            _needStayAfkBehaviorTreeEventSender = needStayAfkBehaviorTreeEventSender;
            _abilityEventListener = _entity.Get<AbilityEventListener>();
            _castPoint ??= new GameObject().transform;
        }

        public void OnConditionAbort()
        {
            _needCastNewSpell = true;
            _abilityEventListener.AbilityEnded -= OnAbilityEnded;
            _currentUsedAttackAbility?.StopCast();
        }

        public void OnStart()
        {
            _entity.Get<Animation>().Animator.SetBool(IsAttacking, true);
        }

        public void OnExit()
        {
            _entity.Get<Animation>().Animator.SetBool(IsAttacking, false);
            //  _currentUsedAttackAbility?.StopCast();
        }

        private void OnAbilityStartCastSpell()
        {
            _currentUsedAttackAbility?.StopCast();
        }

        public TaskStatus ExecuteAction()
        {
            List<AbilityChain> abilityChains = _entity.Get<EnemyCharacterSetup>().MoveSetSetup.AbilityChains;
            if (abilityChains.Count == 0)
            {
                return TaskStatus.Success;
            }

            _entity.Get<IMovement>().Move(Vector3.zero);

            if (_needCastNewSpell == false && _currentUsedAttackAbility is {IsCasted: true})
            {
                return TaskStatus.Running;
            }

            _abilityEventListener.AbilityEnded -= OnAbilityEnded;
            List<AttackAbilitySetup> attackSetups = abilityChains[_previousChainIndex].AbilitySetups;

            if (_currentUsedAttackAbility != null && _previousIndex >= attackSetups.Count)
            {
                _currentUsedAttackAbility = null;
                _previousIndex = 0;
                _needStayAfkBehaviorTreeEventSender?.SendEvent(abilityChains[_previousChainIndex].AfkTimeAfterChain);
                _previousChainIndex++;
                _previousChainIndex %= abilityChains.Count;

                return TaskStatus.Success;
            }

            AttackAbilitySetup currentAttackSetup = attackSetups[_previousIndex];

            _previousIndex++;

            _currentUsedAttackAbility = new Ability(new AbilityDataContainer()
            {
                AbilitySetup = currentAttackSetup, Animator = _entity.Get<Animation>().Animator,
                AbilityEventListener = _abilityEventListener
            }, AttackStateNames[_previousAttackStateNameIndex++]);

            _previousAttackStateNameIndex %= AttackStateNames.Count;
            
            _castPoint.position = _entity.transform.position;
            _castPoint.forward = _entity.transform.forward;
            _currentUsedAttackAbility.CastSpell(_castPoint, _entity);
            _needCastNewSpell = false;
            _abilityEventListener.AbilityEnded += OnAbilityEnded;
            return TaskStatus.Running;
        }

        private void OnEnemyDead(IDying obj)
        {
            OnAbilityEnded();
        }

        private void OnAbilityEnded()
        {
            float afkTimeAfterAbility = _currentUsedAttackAbility.AbilitySetup.AbilityDataSetup.AfkTimeAfterAbility;

            if (afkTimeAfterAbility > 0)
            {
                _needStayAfkBehaviorTreeEventSender?.SendEvent(afkTimeAfterAbility);
            }

            _needCastNewSpell = true;
            _abilityEventListener.AbilityEnded -= OnAbilityEnded;
        }
    }
}