using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.ResourceFolder;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class MoveToRandomPosition : IGameAction
    {
        [SerializeField] private Enemy.Enemy _enemy;
        [SerializeField] private float _maxAngle;

        private CalculatorNavMeshPath _calculatorNavMeshPath;
        private InCirclePointFinderByRaycast _circlePointFinder;
        private Vector3 _desiredPoint;

        public MoveToRandomPosition(Enemy.Enemy enemy)
        {
            _enemy = enemy;
        }

        public void OnStart()
        {
            _calculatorNavMeshPath ??= new CalculatorNavMeshPath();
            _circlePointFinder ??= new InCirclePointFinderByRaycast();
            _desiredPoint = _circlePointFinder.FindFreePointInCircle(_enemy.transform.position,
                4f,4f);
            _desiredPoint = _calculatorNavMeshPath.GetSamplePosition(_desiredPoint);
        }

        public TaskStatus ExecuteAction()
        {
            if (_enemy.EnemyComponents.NpcMovement.CanMove == false) return TaskStatus.Success;

            Vector3 direction = _calculatorNavMeshPath.GetDirectionToNextPoint(_enemy.transform.position,
                _desiredPoint);
            _enemy.EnemyComponents.NpcMovement.Move(direction);
            Vector3 currentDirection = _enemy.transform.forward;
            Vector3 desiredDirection = direction.normalized;

            if (desiredDirection == Vector3.zero || Vector3.Distance(_enemy.transform.position,_desiredPoint) < 1f)
                return TaskStatus.Success;

            float rotationSpeed = _maxAngle * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(currentDirection, _enemy.Target.transform.position - _enemy.transform.position, rotationSpeed, 0f);
            newDirection.y = 0;
            _enemy.transform.rotation = Quaternion.LookRotation(newDirection);
            return TaskStatus.Running;
        }
    }
}