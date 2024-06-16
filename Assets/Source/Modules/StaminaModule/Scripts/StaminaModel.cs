using System;
using UnityEngine;

namespace Source.Modules.StaminaModule.Scripts
{
    public class StaminaModel
    {
        public event Action<float> StaminaUpdated;

        public StaminaModel(float maxStamina)
        {
            MaxStamina = maxStamina;
            CurrentStamina = MaxStamina;
        }

        public float MaxStamina { get; private set; }

        public float CurrentStamina { get; private set; }

        public void UpdateStamina(float value)
        {
            CurrentStamina += value;
            CurrentStamina = Mathf.Clamp(CurrentStamina,0, MaxStamina);
            StaminaUpdated?.Invoke(value);
        }
    }
}