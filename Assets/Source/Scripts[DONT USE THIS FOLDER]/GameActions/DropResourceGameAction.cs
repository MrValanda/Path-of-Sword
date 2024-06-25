using System;
using BehaviorDesigner.Runtime.Tasks;
using Cysharp.Threading.Tasks;
using Source.Modules.BehaviorTreeModule;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class DropResourceGameAction : IGameAction
    {
        [SerializeField] private Enemy.Enemy _enemy;

        private double _previousHealth;

        public DropResourceGameAction(Enemy.Enemy enemy)
        {
            _enemy = enemy;
        }

        public TaskStatus ExecuteAction()
        {
            if (_enemy.EnemyCharacterSetup.WhenTakeDamageDropListSetup == null) return TaskStatus.Success;
            if (_enemy.CurrentHealth > _previousHealth)
            {
                _previousHealth = _enemy.CurrentHealth;
            }
            else if (_previousHealth == 0)
            {
                _previousHealth = _enemy.DefaultHealth();
            }

            double receivedDamage = _previousHealth - _enemy.CurrentHealth;
            if (receivedDamage >=
                _enemy.EnemyCharacterSetup.WhenTakeDamageDropListSetup.ReceiveDamageToTryDropping)
            {
                int howMuchNeedDropLoot = (int) (receivedDamage / _enemy.EnemyCharacterSetup.WhenTakeDamageDropListSetup
                    .ReceiveDamageToTryDropping);

                _previousHealth = _enemy.CurrentHealth;
                for (int i = 0; i < howMuchNeedDropLoot; i++)
                {
                    DropLoot().Forget();
                }
            }


            return TaskStatus.Success;
        }

        private async UniTaskVoid DropLoot()
        {
            //DROP LOOT
        }
    }
}