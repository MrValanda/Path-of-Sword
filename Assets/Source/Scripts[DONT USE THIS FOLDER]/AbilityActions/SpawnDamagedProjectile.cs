using System;
using System.Linq;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.Scripts.Enemy;
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
        [SerializeField, Min(0), MaxValue(1)] private int _handIndex;
        [SerializeField] private float _projectileSpeed;

        public void ExecuteAction(Transform castPoint, Enemy.Enemy abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            Transform spawnProjectilePosition =
                _handIndex == 0
                    ? abilityCaster.EnemyWeaponLeftHand.transform
                    : abilityCaster.EnemyWeaponRightHand.transform;


            EnemyWeapon currentEnemyWeapon = _handIndex == 0
                ? abilityCaster.EnemyWeaponLeftHand
                : abilityCaster.EnemyWeaponRightHand;
            
            if (currentEnemyWeapon is RangeEnemyWeapon enemyWeapon)
            {
                spawnProjectilePosition = enemyWeapon.SpawnProjectilePosition;
            }

            DamagedProjectileLogic spawn = LeanPool.Spawn(_damagedProjectile, spawnProjectilePosition);
            spawn.transform.parent = null;
            spawn.transform.position = spawn.transform.position + Vector3.one * 0.5f;
            spawn.transform.localScale = _damagedProjectile.transform.localScale;

            float maxDistance = ((RectangleIndicatorDataSetup) baseAbilitySetup.IndicatorDataSetup).MaxDistance;

            abilityCaster.UpdateDamage(baseAbilitySetup.Damage);
            spawn.Init((float) abilityCaster.CurrentDamage,
                _damageableContainerSetup.DamageableTypes.Select(x => x.Type).ToList(),
                maxDistance, null,
                _projectileSpeed, _layersToDestoryProjectile);

            spawn.Move(abilityCaster.transform.forward * _projectileSpeed);
        }
    }
}