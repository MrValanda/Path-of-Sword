using System;
using System.Linq;
using Lean.Pool;
using Source.Modules.EnemyModule.Scripts;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.ProjectilesLogic;
using Source.Scripts.Setups;
using Source.Scripts.Setups.Characters;
using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    [Serializable]
    public class SpawnDamagedProjectile : IAbilityAction
    {
        [SerializeField] private DamagedProjectileLogic _damagedProjectile;
        [SerializeField] private DamageableContainerSetup _damageableContainerSetup;
        [SerializeField] private LayerMask _layersToDestoryProjectile;
        [SerializeField] private float _projectileSpeed;

        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            Transform spawnProjectilePosition = abilityCaster.Get<WeaponLocator>().transform;

            DamagedProjectileLogic spawn = LeanPool.Spawn(_damagedProjectile, spawnProjectilePosition);
            spawn.transform.parent = null;
            spawn.transform.position += Vector3.one * 0.5f;
            spawn.transform.localScale = _damagedProjectile.transform.localScale;

            float maxDistance = ((RectangleIndicatorDataSetup) baseAbilitySetup.IndicatorDataSetup).MaxDistance;

            spawn.Init(abilityCaster.Get<DamageCalculator>().CalculateDamage(baseAbilitySetup.Damage),
                _damageableContainerSetup.DamageableTypes.Select(x => x.Type).ToList(),
                maxDistance, null,
                _projectileSpeed, _layersToDestoryProjectile);

            spawn.Move(abilityCaster.transform.forward * _projectileSpeed);
        }
    }
}