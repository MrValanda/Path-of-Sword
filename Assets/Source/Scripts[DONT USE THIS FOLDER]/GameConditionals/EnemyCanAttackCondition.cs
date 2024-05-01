using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.Enemy;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameConditionals
{
    [Serializable]
    public class EnemyCanAttackCondition : IGameCondition
    {
        [SerializeField] private Enemy.Enemy _enemy;
        [SerializeField] private LayerMask _layersObstacles;

        public EnemyCanAttackCondition(Enemy.Enemy enemy, LayerMask layersObstacles)
        {
            _enemy = enemy;
            _layersObstacles = layersObstacles;
        }

        public TaskStatus GetConditionStatus()
        {
            if (_enemy.AbilityCaster.IsAbilityProcessed) return TaskStatus.Failure;
            Vector3 attackerPosition = _enemy.transform.position;
            Vector3 targetPosition = _enemy.Target.transform.position;
            Vector3 directionToTarget = targetPosition - attackerPosition;

            bool targetOutAttackRadius = directionToTarget.sqrMagnitude >=
                                         _enemy.EnemyCharacterSetup.AttackRadius *
                                         _enemy.EnemyCharacterSetup.AttackRadius;

            bool containsObstacleBetweenTarget = Physics.Raycast(attackerPosition, targetPosition - attackerPosition,
                Mathf.Min(_enemy.EnemyCharacterSetup.AttackRadius, directionToTarget.magnitude), _layersObstacles);

            if (targetOutAttackRadius || containsObstacleBetweenTarget)
            {
                AbilityUseData addOrGetComponent = _enemy.ComponentContainer.AddOrGetComponent<AbilityUseData>();

                return addOrGetComponent.CurrentAbility is {IsCasted: true} ? TaskStatus.Success : TaskStatus.Failure;
            }

            return TaskStatus.Success;
        }
    }
}