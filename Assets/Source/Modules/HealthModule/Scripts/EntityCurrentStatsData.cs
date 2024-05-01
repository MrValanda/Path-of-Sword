using UnityEngine;

namespace Source.Scripts.VisitableComponents
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