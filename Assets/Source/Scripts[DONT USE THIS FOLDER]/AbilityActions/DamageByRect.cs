using System;
using System.Collections.Generic;
using System.Linq;
using Source.Scripts.GameExtensions;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using Source.Scripts.Setups.Characters;
using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    [Serializable]
    public class DamageByRect : IAbilityAction
    {
        [SerializeField] private LayerMask _layerObstacles;
        [SerializeField] private DamageableContainerSetup _damageableContainerSetup;

        public DamageByRect()
        {
        }

        public DamageByRect(LayerMask layerObstacles, DamageableContainerSetup damageableContainerSetup)
        {
            _layerObstacles = layerObstacles;
            _damageableContainerSetup = damageableContainerSetup;
        }

        public void ExecuteAction(Transform castPoint, Enemy.Enemy abilityCaster, AbilityDataSetup baseAbilityDataSetup)
        {
            if (baseAbilityDataSetup.IndicatorDataSetup is not RectangleIndicatorDataSetup rectangleIndicatorDataSetup)
            {
                Debug.LogError(
                    $"{baseAbilityDataSetup.IndicatorDataSetup} is not {nameof(RectangleIndicatorDataSetup)}");
                return;
            }

            abilityCaster.UpdateDamage(baseAbilityDataSetup.Damage);
            DamageExecute(castPoint, rectangleIndicatorDataSetup.Width, rectangleIndicatorDataSetup.MaxDistance,
                (float) abilityCaster.CurrentDamage, abilityCaster.ComponentContainer.GetComponent<IDamageable>());
        }

        public void DamageExecute(Transform castPoint, float width, float distance, float damage,
            IDamageable sender = null)
        {
            Collider[] overlapBox =
                Physics.OverlapBox(castPoint.position, new Vector3(width / 2, width / 2, width / 2 * distance),
                    castPoint.rotation);

            List<IDamageable> unitsToAttack = overlapBox
                .Where(x => CheckUnitRect(castPoint, width, distance, x))
                .Where(x => x.CanAttackUnit(_damageableContainerSetup))
                .Select(x => x.GetComponent<IDamageable>()).ToList();

            foreach (IDamageable damageable in unitsToAttack)
            {
                if (ReferenceEquals(damageable, sender)) continue;

                damageable.ApplyDamage(damage);
            }
        }

        private bool CheckUnitRect(Transform castPoint, float width, float distance, Collider unit)
        {
            return Physics.BoxCast(castPoint.position, new Vector3(width / 2, width / 2, width / 2),
                castPoint.forward, castPoint.rotation, distance, _layerObstacles) == false;
        }
    }
}