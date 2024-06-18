using System;
using Source.Modules.CombatModule.Scripts.Parry;
using Source.Modules.StaggerModule.Scripts;
using Source.Scripts.EntityLogic;

namespace Source.Modules.StaminaModule.Scripts
{
    public class StaminaController : IDisposable
    {
        private readonly StaminaView _staminaView;
        private readonly Entity _ownerEntity;

        public StaminaController(Entity ownerEntity,StaminaView staminaView)
        {
            _ownerEntity = ownerEntity;
            _staminaView = staminaView;
            _ownerEntity.Get<StaminaModel>().StaminaUpdated += OnStaminaUpdate;
            _ownerEntity.ComponentAdded += OnComponentAdded;
        }

        public void Dispose()
        {
            _ownerEntity.Get<StaminaModel>().StaminaUpdated -= OnStaminaUpdate;
            _ownerEntity.ComponentAdded -= OnComponentAdded;
        }

        public void ResetStamina()
        {
            _ownerEntity.Get<StaminaModel>().UpdateStamina(_ownerEntity.Get<StaminaModel>().MaxStamina);
        }

        private void OnComponentAdded(Type obj)
        {
            if (obj == typeof(ProtectionImpactOneFrame))
            {
                _ownerEntity.Get<StaminaModel>().UpdateStamina(-10);
            }
        }

        private void OnStaminaUpdate(float obj)
        {
            StaminaModel staminaModel = _ownerEntity.Get<StaminaModel>();
            if (staminaModel.CurrentStamina <= 0)
            {
                _ownerEntity.Add(new ParryBrokenComponent());
            }
            _staminaView.UpdateValue(staminaModel.CurrentStamina / staminaModel.MaxStamina);
        }
    }
}