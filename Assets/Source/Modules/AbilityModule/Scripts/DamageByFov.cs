using System;
using System.Collections.Generic;
using System.Linq;
using Source.Modules.EnemyModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.GameConditionals;
using Source.Scripts.GameExtensions;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using Source.Scripts.Setups.Characters;
using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    [Serializable]
    public class DamageByFov : IAbilityAction
    {
        [SerializeField] private LayerMask _layerObstacles;
        [SerializeField] private DamageableContainerSetup _damageableContainerSetup;
        private FieldOfViewChecker _fieldOfViewChecker;

        public DamageByFov()
        {
        }

        public DamageByFov(LayerMask layerObstacles, DamageableContainerSetup damageableContainerSetup)
        {
            _layerObstacles = layerObstacles;
            _damageableContainerSetup = damageableContainerSetup;
        }

        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilityDataSetup)
        {
            if (baseAbilityDataSetup.IndicatorDataSetup is not ConeIndicatorDataSetup coneIndicatorDataSetup)
            {
                Debug.LogError($"{baseAbilityDataSetup.IndicatorDataSetup} is not {nameof(ConeIndicatorDataSetup)}");
                return;
            }


            DamageExecute(castPoint, coneIndicatorDataSetup.Radius, coneIndicatorDataSetup.Angle,
                abilityCaster.Get<DamageCalculator>().CalculateValue(baseAbilityDataSetup.Damage),
                abilityCaster.Get<IDamageable>());
        }

        public void DamageExecute(Transform castPoint, float radius, float angle, float damage,
            IDamageable sender = null)
        {
            _fieldOfViewChecker ??= new FieldOfViewChecker();
            Collider[] overlapSphere = Physics.OverlapSphere(castPoint.position, radius);
            List<IDamageable> unitsToAttack = overlapSphere
                .Where(x => CheckUnitInFieldOfView(castPoint, radius, angle, x))
                .Where(x => x.CanAttackUnit(_damageableContainerSetup))
                .Select(x => x.GetComponent<IDamageable>()).ToList();

            foreach (IDamageable damageable in unitsToAttack)
            {
                if (ReferenceEquals(damageable, sender)) continue;

                damageable.ApplyDamage(damage);
            }
        }

        private bool CheckUnitInFieldOfView(Transform castPoint, float radius, float angle, Collider unit)
        {
            return _fieldOfViewChecker.Check(castPoint, unit.transform, _layerObstacles, radius,
                angle);
        }
    }
}