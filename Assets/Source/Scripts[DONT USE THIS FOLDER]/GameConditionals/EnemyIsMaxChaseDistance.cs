using System;
using BehaviorDesigner.Runtime.Tasks;
using Sirenix.Serialization;
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
                   _enemy.EnemyCharacterSetup.MaxChaseDistance
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}