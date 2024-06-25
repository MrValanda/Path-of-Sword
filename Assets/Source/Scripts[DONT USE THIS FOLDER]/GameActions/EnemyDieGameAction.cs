using System;
using BehaviorDesigner.Runtime.Tasks;
using Cysharp.Threading.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class EnemyDieGameAction : IGameAction
    {
        [SerializeField] private Enemy.Enemy _enemy;


        public EnemyDieGameAction(Enemy.Enemy enemy)
        {
            _enemy = enemy;
        }

        public TaskStatus ExecuteAction()
        {
            _enemy.transform.forward = (_enemy.Target.transform.position - _enemy.transform.position).normalized;
            _enemy.EnemyComponents.Animation.ResetTriggers();
            _enemy.EnemyComponents.Animation.SetTrigger("Die");
            _enemy.GetComponent<CapsuleCollider>().enabled = false;
            _enemy.EnemyWeaponLeftHand.transform.parent = _enemy.EnemyComponents.Animation.Animator.transform;
            _enemy.EnemyWeaponLeftHand.Rigidbody.isKinematic = false;
            _enemy.EnemyWeaponLeftHand.Collider.isTrigger = false;
            _enemy.EnemyWeaponLeftHand.Collider.enabled = true;

            if (_enemy.ComponentContainer.TryGetComponent(out AbilityUseData abilityUseData))
            {
                abilityUseData.CurrentAbility?.StopCast();
            }

            _enemy.EnemyWeaponLeftHand.Rigidbody.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f),
                Random.Range(-1f, 1f)));

            _enemy.EnemyWeaponRightHand.transform.parent = _enemy.EnemyComponents.Animation.Animator.transform;
            _enemy.EnemyWeaponRightHand.Rigidbody.isKinematic = false;
            _enemy.EnemyWeaponRightHand.Collider.isTrigger = false;
            _enemy.EnemyWeaponRightHand.Collider.enabled = true;
            _enemy.EnemyWeaponRightHand.Rigidbody.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f),
                Random.Range(-1f, 1f)));

            DropLoot().Forget();

            return TaskStatus.Success;
        }

        private async UniTaskVoid DropLoot()
        {
            //DROP LOOT
        }
    }
}