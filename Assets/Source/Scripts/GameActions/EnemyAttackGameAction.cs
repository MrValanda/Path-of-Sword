using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.Abilities;
using Source.Scripts.AnimationEventListeners;
using Source.Scripts.BehaviorTreeEventSenders;
using Source.Scripts.Enemy;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class EnemyAttackGameAction : IGameAction
    {
        private static readonly List<string> AttackStateNames = new()
        {
            "Attack",
            "Attack2",
        };

        [SerializeField] private Enemy.Enemy _enemy;
        [SerializeField] private AbilityEventListener _abilityEventListener;
        [SerializeField] private NeedStayAfkBehaviorTreeEventSender _needStayAfkBehaviorTreeEventSender;

        private int _previousIndex;
        private int _previousChainIndex;
        private int _previousAttackStateNameIndex;
        private Ability _currentUsedAttackAbility;
        private Transform _castPoint;
        private float _lastAttackTime;
        private bool _needCastNewSpell;

        public EnemyAttackGameAction(Enemy.Enemy enemy,
            NeedStayAfkBehaviorTreeEventSender needStayAfkBehaviorTreeEventSender)
        {
            _enemy = enemy;
            _enemy.ComponentContainer.GetComponent<IDying>().Dead += OnEnemyDead;
            _enemy.AbilityCaster.StartCastSpell += OnAbilityStartCastSpell;
            _needStayAfkBehaviorTreeEventSender = needStayAfkBehaviorTreeEventSender;
        }

        public void OnStart()
        {
            _castPoint ??= new GameObject().transform;
            _enemy.AbilityCaster.BlockCast();
        }

        public void OnExit()
        {
          //  _currentUsedAttackAbility?.StopCast();
        }
        
        private void OnAbilityStartCastSpell()
        {
            _enemy.AbilityCaster.UnBlockCast();
            _currentUsedAttackAbility?.StopCast();
        }

        public TaskStatus ExecuteAction()
        {
            if (_enemy.AbilityCaster.IsAbilityProcessed)
            {
                _currentUsedAttackAbility?.StopCast();
                _enemy.AbilityCaster.UnBlockCast();
                return TaskStatus.Success;
            }

            if (_enemy.EnemyCharacterSetup.MoveSetSetup.AbilityChains.Count == 0)
            {
                _enemy.AbilityCaster.UnBlockCast();
                return TaskStatus.Success;
            }

            _enemy.EnemyComponents.NpcMovement.Move(Vector3.zero);

            if (_needCastNewSpell == false && _currentUsedAttackAbility is {IsCasted: true})
            {
                return TaskStatus.Running;
            }

            _abilityEventListener.AbilityEnded -= OnAbilityEnded;
            List<AttackAbilitySetup> attackSetups =
                _enemy.EnemyCharacterSetup.MoveSetSetup.AbilityChains[_previousChainIndex].AbilitySetups;

            if (_currentUsedAttackAbility != null && _previousIndex >= attackSetups.Count)
            {
                _currentUsedAttackAbility = null;
                _previousIndex = 0;
                _needStayAfkBehaviorTreeEventSender.SendEvent(_enemy.EnemyCharacterSetup.MoveSetSetup
                    .AbilityChains[_previousChainIndex].AfkTimeAfterChain);
                _previousChainIndex++;
                _previousChainIndex %= _enemy.EnemyCharacterSetup.MoveSetSetup.AbilityChains.Count;

                _enemy.AbilityCaster.UnBlockCast();
                return TaskStatus.Success;
            }

            AttackAbilitySetup currentAttackSetup = attackSetups[_previousIndex];

            _previousIndex++;

            _currentUsedAttackAbility = new Ability(new AbilityDataContainer()
            {
                AbilitySetup = currentAttackSetup, Animator = _enemy.EnemyComponents.Animation.Animator,
                AbilityEventListener = _abilityEventListener
            }, AttackStateNames[_previousAttackStateNameIndex++]);

            _previousAttackStateNameIndex %= AttackStateNames.Count;

            AbilityUseData addOrGetComponent = _enemy.ComponentContainer.AddOrGetComponent<AbilityUseData>();
            addOrGetComponent.CurrentAbility = _currentUsedAttackAbility;

            _castPoint.position = _enemy.transform.position;
            _castPoint.forward = _enemy.transform.forward;
            _lastAttackTime = Time.time;
            _currentUsedAttackAbility.CastSpell(_castPoint, _enemy);
            _needCastNewSpell = false;
            _enemy.UpdateDamage(currentAttackSetup.AbilityDataSetup.Damage);
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
                _needStayAfkBehaviorTreeEventSender.SendEvent(afkTimeAfterAbility);
            }

            _needCastNewSpell = true;
            _abilityEventListener.AbilityEnded -= OnAbilityEnded;
        }
    }
}