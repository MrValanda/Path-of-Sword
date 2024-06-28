using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.DamageableFindersModule;
using Source.Scripts.Enemy;
using Source.Scripts.EntityLogic;
using Source.Scripts.Setups.Characters;
using UnityEngine;

namespace Source.Scripts.GameConditionals
{
    [Serializable]
    public class EntityCanAttackCondition : IGameCondition
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private LayerMask _layersObstacles;

        public EntityCanAttackCondition(Entity entity, LayerMask layersObstacles)
        {
            _entity = entity;
            _layersObstacles = layersObstacles;
        }

        public TaskStatus GetConditionStatus()
        {
            Vector3 attackerPosition = _entity.transform.position;
            Vector3 targetPosition = _entity.Get<DamageableSelector>().SelectedDamageable.transform.position;
            Vector3 directionToTarget = targetPosition - attackerPosition;
            EnemyCharacterSetup enemyCharacterSetup = _entity.Get<EnemyCharacterSetup>();
            bool targetOutAttackRadius = directionToTarget.sqrMagnitude >=
                                         enemyCharacterSetup.AttackRadius *
                                         enemyCharacterSetup.AttackRadius;

            bool containsObstacleBetweenTarget = Physics.Raycast(attackerPosition, targetPosition - attackerPosition,
                Mathf.Min(enemyCharacterSetup.AttackRadius, directionToTarget.magnitude), _layersObstacles);

            if (targetOutAttackRadius || containsObstacleBetweenTarget)
            {
                AbilityUseData addOrGetComponent = _entity.AddOrGet<AbilityUseData>();

                return addOrGetComponent.CurrentAbility is {IsCasted: true} ? TaskStatus.Success : TaskStatus.Failure;
            }

            return TaskStatus.Success;
        }
    }
}