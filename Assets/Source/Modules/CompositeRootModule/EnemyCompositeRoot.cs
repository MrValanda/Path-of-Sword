using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Modules.CombatModule.Scripts;
using Source.Modules.DamageableFindersModule;
using Source.Modules.EnemyModule.Scripts;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.BehaviorTreeEventSenders;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups.Characters;
using UnityEngine;

namespace Source.Modules.CompositeRootModule
{
    public class EnemyCompositeRoot : MonoBehaviour
    {
        [SerializeField] private Entity _enemyEntity;
        [SerializeField] private BehaviorTreeCompositeRoot _behaviorTreeCompositeRoot;
        [SerializeField,TabGroup("Equipment")] private Equipment _equipment;
        [SerializeField] private Transform _orientation;
        [SerializeField] private DamageableContainerSetup _damageableContainerSetup;
        [SerializeField] private EnemyCharacterSetup _enemyCharacterSetup;

        public void Compose()
        {
            Container<IGameCondition> conditionsContainer =
                new Container<IGameCondition>(new List<IGameCondition>()
                {
                    new InfinitySuccessCondition()
                });
            AttackStateComponentData attackStateComponentData =
                new AttackStateComponentData(_enemyEntity.transform, 0.1f, _orientation, conditionsContainer);

            _equipment.Initialize(_enemyEntity, _damageableContainerSetup);
            
            _enemyEntity.Add(attackStateComponentData);
            _enemyEntity.Add(_equipment);


            DamageableFinder damageableFinder = new DamageableFinder(_damageableContainerSetup,
                _enemyEntity.Get<DamageableFinderCollider>().Collider);

            _enemyEntity.Add(damageableFinder);

            DamageableSelector damageableSelector = new DamageableSelector();
            damageableSelector.Initialize(_enemyEntity.transform, damageableFinder);
            _enemyEntity.Add(damageableSelector);
            _enemyEntity.Add(_enemyCharacterSetup);
            _enemyEntity.Add(new DamageCalculator(_enemyCharacterSetup.DamageMultiplier, 1));
            _behaviorTreeCompositeRoot.Compose();
        }
    }
}