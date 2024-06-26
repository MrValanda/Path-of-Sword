using Sirenix.OdinInspector;
using Source.Modules.DamageableFindersModule;
using Source.Modules.EnemyModule.Scripts;
using Source.Modules.HealthModule.Scripts;
using Source.Modules.StaggerModule.Scripts;
using Source.Modules.StaminaModule.Scripts;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Setups.Characters;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Abilities;
using UnityEngine;

namespace Source.Modules.CompositeRootModule
{
    public class EnemyCompositeRoot : MonoBehaviour
    {
        [SerializeField] private Entity _enemyEntity;
        [SerializeField] private BehaviorTreeCompositeRoot _behaviorTreeCompositeRoot;

        [SerializeField, TabGroup("Equipment")]
        private Equipment _equipment;

        [SerializeField] private DamageableContainerSetup _damageableContainerSetup;
        [SerializeField] private EnemyCharacterSetup _enemyCharacterSetup;
        [SerializeField] private HealthView _enemyHealthView;
        [SerializeField] private StaminaView _enemyStaminaView;

        public void Compose()
        {
            _equipment.Initialize(_enemyEntity, _damageableContainerSetup);

            _enemyEntity.Add(_equipment);


            DamageableFinder damageableFinder = new(
                _damageableContainerSetup,
                _enemyEntity.Get<DamageableFinderCollider>().Collider);

            _enemyEntity.Add(damageableFinder);

            DamageableSelector damageableSelector = new();
            damageableSelector.Initialize(_enemyEntity.transform, damageableFinder);

            _enemyEntity.Add(damageableSelector);
            _enemyEntity.Add(_enemyCharacterSetup);
            _enemyEntity.Add(new DamageCalculator(_enemyCharacterSetup.DamageMultiplier, 1));

            AbilityCaster abilityCaster = new();
            abilityCaster.Init(_enemyCharacterSetup.AbilityContainerSetup, _enemyEntity);

            _enemyEntity.Add(abilityCaster);
            StaggerHandler staggerHandler = new();
            staggerHandler.Initialize(_enemyEntity);
            _enemyEntity.Add(staggerHandler);

            HealthController healthController = new(_enemyHealthView, _enemyEntity.Get<HealthComponent>());
            _enemyEntity.Add(healthController);

            _enemyEntity.Add(new StaminaModel(200));
            _enemyEntity.Add(new StaminaController(_enemyEntity, _enemyStaminaView));
            
            _behaviorTreeCompositeRoot.Compose();
        }
    }
}