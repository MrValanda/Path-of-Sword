using System.Collections.Generic;
using Cinemachine;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.CombatModule.Scripts;
using Source.Modules.CombatModule.Scripts.Parry;
using Source.Modules.HealthModule.Scripts;
using Source.Modules.LockOnTargetModule.Scripts;
using Source.Modules.MovementModule.Scripts;
using Source.Modules.StaminaModule.Scripts;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Setups.Characters;
using UnityEngine;

namespace Source.Modules.CompositeRootModule
{
    public class PlayerCompositeRoot : MonoBehaviour
    {
        [SerializeField, InlineEditor(InlineEditorModes.LargePreview)]
        private Entity _playerEntity;

        [SerializeField, TabGroup("Equipment")]
        private Equipment _equipment;

        [SerializeField] private Transform _orientation;
        [SerializeField] private InputMouseDownCondition _inputMouseDownCondition;
        [SerializeField] private ParryHandlerData _parryHandlerData;
        [SerializeField] private CinemachineVirtualCamera _lockOnTargetCamera;
        [SerializeField] private CinemachineFreeLook _freeLookCamera;
        [SerializeField] private DamageableContainerSetup _damageableContainerSetup;
        [SerializeField] private HealthView _playerHealthView;
        [SerializeField] private StaminaView _playerStaminaView;
        [SerializeField] private bool _spawnPlayer;

        public void Compose()
        {
            Entity entity = _spawnPlayer
                ? LeanPool.Spawn(_playerEntity, transform.position, Quaternion.identity)
                : _playerEntity;

            InitMoveSet(entity);
            
            InitStamina(entity);
            
            _equipment.Initialize(entity, _damageableContainerSetup);
            entity.Add(_equipment);

            ParryHandler parryHandler = new();
            parryHandler.Initialize(entity, _parryHandlerData);
            entity.Add(parryHandler);

            MovementStateData movementStateData = new(entity.transform, _orientation, 0.1f, 10);

            entity.Add(movementStateData);

            DodgeStateData dodgeStateData = new(_orientation, entity.transform);
            entity.Add(dodgeStateData);

            _freeLookCamera.Follow = entity.Get<CameraFollowTarget>().transform;
            _freeLookCamera.LookAt = entity.Get<CameraFollowTarget>().transform;
            _lockOnTargetCamera.Follow = entity.Get<CameraFollowTarget>().transform;
            entity.Get<LockOnSelector>().Initialize(_lockOnTargetCamera);

            HealthController healthController = new(_playerHealthView, entity.Get<HealthComponent>());
            entity.Add(healthController);
        }

        private void InitStamina(Entity entity)
        {
            entity.Add(new StaminaModel(100));
            entity.Add(new StaminaController(entity,_playerStaminaView));
        }
        
        private void InitMoveSet(Entity entity)
        {
            Dictionary<MoveSetType, AttackStateComponentData> stateComponentDatas =
                new Dictionary<MoveSetType, AttackStateComponentData>();

            stateComponentDatas[MoveSetType.IdleType] = GetAttackDataByMoveSetType(MoveSetType.IdleType,
                new List<IGameCondition>() {_inputMouseDownCondition},
                entity.transform);

            stateComponentDatas[MoveSetType.AfterRoll] = GetAttackDataByMoveSetType(MoveSetType.AfterRoll,
                new List<IGameCondition>() {new InfinityFailureCondition()},
                entity.transform);

            AttackStateDataContainer attackStateDataContainer = new(stateComponentDatas);
            entity.Add(attackStateDataContainer);
        }

        private AttackStateComponentData GetAttackDataByMoveSetType(MoveSetType moveSetType,
            List<IGameCondition> gameConditions, Transform whoWasRotate)
        {
            Container<IGameCondition> conditionsContainer =
                new Container<IGameCondition>(gameConditions);

            AttackStateComponentData attackStateComponentData =
                new(whoWasRotate, 0.1f, _orientation, conditionsContainer);

            return attackStateComponentData;
        }
    }
}