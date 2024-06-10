using System;
using Lean.Pool;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    [Serializable]
    public class AbilityVFXAction : IAbilityAction
    {
        [SerializeField] private ParticleSystem _abilityParticle;
        
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            LeanPool.Spawn(_abilityParticle, castPoint.position, _abilityParticle.transform.rotation);
        }

        // public void ExecuteAction(Transform castPoint, Enemy.Enemy abilityCaster, AbilityDataSetup baseAbilitySetup)
        // {
        //     Enemy.Enemy hostileCharacter = abilityCaster;
        //     hostileCharacter.transform.LookAt(hostileCharacter.Target.transform);
        //     float angleStep = baseAbilitySetup.Angle / (_points - 1);
        //     float currentAngle = -baseAbilitySetup.Angle * 0.5f;
        //     
        //     for (int i = 0; i < _points; i++)
        //     {
        //         Vector3 direction = Quaternion.Euler(0f, currentAngle, 0f) * castPoint.forward;
        //         DamagedProjectileLogic spawn = LeanPool.Spawn(_damagedProjectile, castPoint);
        //         spawn.transform.parent = null;
        //         spawn.transform.localScale = _damagedProjectile.transform.localScale;
        //         spawn.Init(baseAbilitySetup.Damage,
        //             abilityCaster.EnemyCharacterSetup.AttakedUnits.DamageableTypes.Select(x => x.Type).ToList(),
        //             baseAbilitySetup.Radius);
        //         Debug.DrawRay(castPoint.position, direction, Color.red, 1f);
        //         spawn.Move(direction.normalized * _projectileSpeed);
        //         
        //         currentAngle += angleStep;
        //     }
        // }
    }
}