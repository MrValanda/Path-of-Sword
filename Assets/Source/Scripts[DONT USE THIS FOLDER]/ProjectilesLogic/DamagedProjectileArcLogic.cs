using Lean.Pool;
using Source.Scripts.AbilityActions;
using Source.Scripts.Setups;
using Source.Scripts.Setups.Characters;
using UnityEngine;

namespace Source.Scripts.ProjectilesLogic
{
    public class DamagedProjectileArcLogic : ProjectileArcLogic
    {
        [SerializeField] private ParticleSystem _reachedTargetVFX;
        private float _damage;
        private DamageableContainerSetup _damageableContainerSetup;

        public void InitDamage(float damage, IndicatorDataSetup indicatorDataSetup, LayerMask obstacles,
            DamageableContainerSetup damageableContainerSetup)
        {
            _damage = damage;
            _indicatorDataSetup = indicatorDataSetup;
            _obstacles = obstacles;
            _damageableContainerSetup = damageableContainerSetup;
        }

        protected override void OnProjectileReachedTarget()
        {
            if (_indicatorDataSetup is not ConeIndicatorDataSetup coneIndicatorDataSetup)
            {
                return;
            }

            DamageByFov damageByFov = new DamageByFov(_obstacles, _damageableContainerSetup);
            damageByFov.DamageExecute(transform, coneIndicatorDataSetup.Radius, coneIndicatorDataSetup.Angle, _damage);
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
            {
                ParticleSystem system = LeanPool.Spawn(_reachedTargetVFX, hit.point + Vector3.one * 0.1f,
                    _reachedTargetVFX.transform.rotation);
                system.transform.localScale = new Vector3(1, 1, 1) * coneIndicatorDataSetup.Radius;
            }
           
            LeanPool.Despawn(this);
        }
    }
}