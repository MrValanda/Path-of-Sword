using System;
using BehaviorDesigner.Runtime.Tasks;
using Sirenix.Serialization;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameConditionals
{
    [Serializable]
    public class EnemyIsMaxChaseDistance : IGameCondition
    {
        [SerializeField] private Enemy.Enemy _enemy;

        public EnemyIsMaxChaseDistance(Enemy.Enemy enemy)
        {
            _enemy = enemy;
        }

        public TaskStatus GetConditionStatus()
        {
            return Vector3.Distance(_enemy.EnemySpawnPoint.transform.position, _enemy.transform.position) >
                 1
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}