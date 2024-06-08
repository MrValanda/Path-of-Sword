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
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Setups.Characters;
using Source.Scripts.VisitableComponents;
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
        [SerializeField] private bool _spawnPlayer;

        public void Compose()
        {
            Entity entity = _spawnPlayer
                ? LeanPool.Spawn(_playerEntity, transform.position, Quaternion.identity)
                : _playerEntity;
            
            Container<IGameCondition> conditionsContainer =
                new Container<IGameCondition>(new List<IGameCondition>()
                {
                    _inputMouseDownCondition
                });

            AttackStateComponentData attackStateComponentData =
                new(entity.transform, 0.1f, _orientation, conditionsContainer);
            entity.Add(attackStateComponentData);

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
    }
}