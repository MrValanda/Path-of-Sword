using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.Abilities;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class MoveEnemyTowardAttackPoint : IGameAction
    {
        [SerializeField] private Enemy.Enemy _enemy;
        [SerializeField] private float _maxAngle;
        private IAttackPointCalculator _attackPointCalculator;

        public MoveEnemyTowardAttackPoint(Enemy.Enemy enemy)
        {
            _enemy = enemy;
        }

        public TaskStatus ExecuteAction()
        {
            if (_enemy.EnemyComponents.NpcMovement.CanMove == false) return TaskStatus.Success;

            _attackPointCalculator ??= _enemy.GetAttackPointCalculator();

            Vector3 direction = _attackPointCalculator.GetDirection();
            _enemy.EnemyComponents.NpcMovement.Move(direction);
            Vector3 currentDirection = _enemy.transform.forward;
            Vector3 desiredDirection = direction.normalized;
            
            if (desiredDirection == Vector3.zero)
                return TaskStatus.Success;
            
            float rotationSpeed = _maxAngle * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(currentDirection, desiredDirection, rotationSpeed, 0f);

            _enemy.transform.rotation = Quaternion.LookRotation(newDirection);
            return TaskStatus.Success;
        }
    }
}