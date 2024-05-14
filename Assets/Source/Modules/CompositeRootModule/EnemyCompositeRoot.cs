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
        [SerializeField,TabGroup("AttackState")] private AttackStateComponentData _attackStateComponentData;
        [SerializeField,TabGroup("Equipment")] private Equipment _equipment;
        [SerializeField] private Transform _orientation;

        public void Compose()
        {
            Container<ICondition> conditionsContainer =
                new Container<ICondition>(new List<ICondition>()
                {
                    new InfinitySuccessCondition()
                });
            _attackStateComponentData.Initialize(_orientation, conditionsContainer);
            
            _equipment.Initialize();
            
            _enemyEntity.Add(_attackStateComponentData);
            _enemyEntity.Add(_equipment);
            _enemyStateMachine.Init();
        }
    }
}