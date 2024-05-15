using UnityEngine;

namespace Source.Modules.HealthModule.Scripts
{
    public class EntityCurrentStatsData
    {
        public float DamageReducePercent
        {
            get => _damageReducePercent;
            set => _damageReducePercent = Mathf.Clamp01(value);
        }

        private float _damageReducePercent;
    }
}