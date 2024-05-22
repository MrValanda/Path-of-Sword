using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class PushEnemyAwayFromTargetGameAction : IGameAction
    {
        [SerializeField] private Enemy.Enemy _enemy;
        [SerializeField] private float _time = 0.5f;
        
        public PushEnemyAwayFromTargetGameAction(Enemy.Enemy enemy)
        {
            _enemy = enemy;
        }

        public TaskStatus ExecuteAction()
        {
            Vector3 direction = (_enemy.transform.position - _enemy.Target.transform.position);
            direction.y = 0;
            direction = direction.normalized;
            _enemy.EnemyComponents.NpcMovement.PushByDirection(direction * _enemy.EnemyCharacterSetup.ForcePush, _time);
            return TaskStatus.Success;
        }
    }
}