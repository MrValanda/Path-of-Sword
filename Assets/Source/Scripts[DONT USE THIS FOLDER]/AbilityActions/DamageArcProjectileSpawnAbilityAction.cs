using System;
using System.Linq;
using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.Scripts.Interfaces;
using Source.Scripts.ProjectilesLogic;
using Source.Scripts.Setups;
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

        public void ExecuteAction(Transform castPoint, Enemy.Enemy abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            abilityCaster.UpdateDamage(baseAbilitySetup.Damage);

            Vector3 transformPosition = abilityCaster.Target.transform.position;
            abilityCaster.transform.DOLookAt(transformPosition, 0.1f).OnComplete(() =>
            {
                DamagedProjectileArcLogic spawnedProjectile =
                    LeanPool.Spawn(_projectileArcLogic, abilityCaster.EnemyWeaponRightHand.transform);
                //   spawnedProjectile.transform.localScale = _projectileArcLogic.transform.localScale;

                spawnedProjectile.InitIndicator(baseAbilitySetup.IndicatorDataSetup,
                    baseAbilitySetup.ObstacleLayers);

                spawnedProjectile.LaunchProjectile(transformPosition, _jumpPower, _duration, _ease, _flyingDelay);

                spawnedProjectile.InitDamage((float) abilityCaster.CurrentDamage, baseAbilitySetup.IndicatorDataSetup,
                    baseAbilitySetup.ObstacleLayers, abilityCaster.EnemyCharacterSetup.AttakedUnits);
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