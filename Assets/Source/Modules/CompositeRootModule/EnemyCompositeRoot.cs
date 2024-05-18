using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Modules.CombatModule.Scripts;
using Source.Modules.StateMachineModule.Scripts;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Modules.CompositeRootModule
{
    public class EnemyCompositeRoot : MonoBehaviour
    {
        [SerializeField] private Entity _enemyEntity;
        [SerializeField] private StateMachine _enemyStateMachine;
        [SerializeField,TabGroup("Equipment")] private Equipment _equipment;
        [SerializeField] private Transform _orientation;

        public void Compose()
        {
            Container<ICondition> conditionsContainer =
                new Container<ICondition>(new List<ICondition>()
                {
                    new InfinitySuccessCondition()
                });
            AttackStateComponentData attackStateComponentData =
                new AttackStateComponentData(_enemyEntity.transform, 0.1f, _orientation, conditionsContainer);

            _equipment.Initialize(_enemyEntity);
            
            _enemyEntity.Add(attackStateComponentData);
            _enemyEntity.Add(_equipment);
            _enemyStateMachine.Init();
        }
    }
}