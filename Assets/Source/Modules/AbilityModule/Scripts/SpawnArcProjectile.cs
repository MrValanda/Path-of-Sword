using System;
using System.Linq;
using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.Modules.DamageableFindersModule;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.ProjectilesLogic;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    [Serializable]
    public class SpawnArcProjectile : IAbilityAction
    {
        [SerializeField] private ProjectileArcLogic _projectileArcLogic;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _duration;
        [SerializeField] private float _flyingDelay;
        [SerializeField] private Ease _ease = Ease.Linear;

        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            ProjectileArcLogic spawnedProjectile =
                LeanPool.Spawn(_projectileArcLogic, abilityCaster.Get<WeaponLocator>().transform.position,
                    Quaternion.identity);
            
            spawnedProjectile.InitIndicator(baseAbilitySetup.IndicatorDataSetup,
                baseAbilitySetup.ObstacleLayers);
            
            Vector3 targetPosition = abilityCaster.Get<DamageableSelector>().SelectedDamageable.transform.position;
            
            spawnedProjectile.LaunchProjectile(targetPosition, _jumpPower, _duration, _ease, _flyingDelay);
        }

        [Button]
        private void CalculateFlyDelay(AnimationClip animationClip, string startEvent, string endEvent)
        {
            _flyingDelay = animationClip.events.FirstOrDefault(x => x.functionName == endEvent).time -
                           animationClip.events.FirstOrDefault(x => x.functionName == startEvent).time;
        }
    }
}