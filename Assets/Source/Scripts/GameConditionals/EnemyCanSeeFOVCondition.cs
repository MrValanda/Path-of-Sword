using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameConditionals
{
    [Serializable]
    public class EnemyCanSeeFOVCondition : IGameCondition
    {
        [SerializeField] private Enemy.Enemy _enemy;
        [SerializeField] private LayerMask _layersObstacles;

        private FieldOfViewChecker _fieldOfViewChecker;

        public EnemyCanSeeFOVCondition(Enemy.Enemy enemy, LayerMask layersObstacles)
        {
            _enemy = enemy;
            _layersObstacles = layersObstacles;
        }

        public TaskStatus GetConditionStatus()
        {
            _fieldOfViewChecker ??= new FieldOfViewChecker();

            bool isInFieldOfView = _fieldOfViewChecker.Check(_enemy.transform, _enemy.Target.transform,
                _layersObstacles,
                _enemy.EnemyCharacterSetup.DetectRadius, _enemy.EnemyCharacterSetup.ViewAngle);

            return isInFieldOfView ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    public class FieldOfViewChecker
    {
        public bool Check(Transform source, Transform target, LayerMask layerMask, float radius, float angle)
        {
            Vector3 directionToTarget = target.position - source.position;
            if (directionToTarget.magnitude <= radius)
            {
                if (Physics.Raycast(source.position, directionToTarget.normalized,
                    directionToTarget.magnitude, layerMask))
                {
                    return false;
                }

                float angleBetweenTargetAndObserver =
                    Mathf.Acos(Mathf.Clamp(Vector3.Dot(source.forward.normalized, directionToTarget.normalized),-1,1)) * Mathf.Rad2Deg;

                if (Mathf.Abs(angleBetweenTargetAndObserver) <= angle * 0.5f)
                {
                    return true;
                }
            }

            return false;
        }
    }
}