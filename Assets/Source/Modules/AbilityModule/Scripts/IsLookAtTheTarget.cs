using System;
using Source.Modules.DamageableFindersModule;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.AbilityConditions
{
    [Serializable]
    public class IsLookAtTheTarget : IAbilityCondition
    {
        [SerializeField] private float _angle;

        public bool CanExecute(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            Vector3 directionToTarget = abilityCaster.Get<DamageableSelector>().SelectedDamageable.transform.position -
                                        abilityCaster.transform.position;
            float angleBetweenTargetAndObserver =
                Mathf.Acos(Mathf.Clamp(
                    Vector3.Dot(abilityCaster.transform.forward.normalized, directionToTarget.normalized), -1, 1)) *
                Mathf.Rad2Deg;

            if (Mathf.Abs(angleBetweenTargetAndObserver) <= _angle * 0.5f)
            {
                return true;
            }

            return false;
        }
    }
}