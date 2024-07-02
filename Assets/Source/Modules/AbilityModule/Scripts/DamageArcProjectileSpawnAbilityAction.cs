using System;
using System.Linq;
using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.Modules.DamageableFindersModule;
using Source.Modules.EnemyModule.Scripts;
using Source.Modules.EnemyModule.Scripts.Setups;
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
    public class DamageArcProjectileSpawnAbilityAction : IAbilityAction
    {
        [SerializeField, InlineEditor(InlineEditorModes.LargePreview)]
        private DamagedProjectileArcLogic _projectileArcLogic;

        [SerializeField] private float _jumpPower;
        [SerializeField] private float _duration;
        [SerializeField] private float _flyingDelay;
        [SerializeField] private Ease _ease = Ease.Linear;

        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            Vector3 transformPosition = abilityCaster.Get<DamageableSelector>().SelectedDamageable.transform.position;
            abilityCaster.transform.DOLookAt(transformPosition, 0.1f).OnComplete(() =>
            {
                DamagedProjectileArcLogic spawnedProjectile =
                    LeanPool.Spawn(_projectileArcLogic, abilityCaster.Get<WeaponLocator>().transform);
                //   spawnedProjectile.transform.localScale = _projectileArcLogic.transform.localScale;

                spawnedProjectile.InitIndicator(baseAbilitySetup.IndicatorDataSetup,
                    baseAbilitySetup.ObstacleLayers);

                spawnedProjectile.LaunchProjectile(transformPosition, _jumpPower, _duration, _ease, _flyingDelay);

                spawnedProjectile.InitDamage(
                    abilityCaster.Get<DamageCalculator>().CalculateValue(baseAbilitySetup.Damage),
                    baseAbilitySetup.IndicatorDataSetup,
                    baseAbilitySetup.ObstacleLayers, abilityCaster.Get<EnemyCharacterSetup>().AttackedUnits);
            });
        }

        [Button]
        private void CalculateFlyDelay(AnimationClip animationClip, string startEvent, string endEvent)
        {
            _flyingDelay = animationClip.events.FirstOrDefault(x => x.functionName == endEvent).time -
                           animationClip.events.FirstOrDefault(x => x.functionName == startEvent).time;
        }
    }
}