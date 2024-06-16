using System;
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

        private void OnComponentAdded(Type obj)
        {
            if (_ownerEntity.Contains<ProtectionImpactOneFrame>())
            {
                _ownerEntity.Get<StaminaModel>().UpdateStamina(-10);
            }
        }

        public void Dispose()
        {
            _ownerEntity.Get<StaminaModel>().StaminaUpdated -= OnStaminaUpdate;
        }

        private void OnStaminaUpdate(float obj)
        {
            StaminaModel staminaModel = _ownerEntity.Get<StaminaModel>();
            _staminaView.UpdateValue(staminaModel.CurrentStamina / staminaModel.MaxStamina);
        }
    }
}