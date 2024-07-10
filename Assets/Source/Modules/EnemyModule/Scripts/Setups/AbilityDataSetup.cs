using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Source.Scripts.AnimationEventNames;
using UnityEditor;
using UnityEngine;

namespace Source.Scripts.Setups
{
    [CreateAssetMenu(fileName = "AbilityDataSetup", menuName = "Setups/AbilityDataSetup")]
    public class AbilityDataSetup : SerializedScriptableObject
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: OdinSerialize] public  IndicatorDataSetup IndicatorDataSetup { get; private set; }
        [field: SerializeField] public float PreparationAttackTime { get; private set; }
        [field: SerializeField] public float StartAttackTime { get; private set; }
        [field: SerializeField] public float AfkTimeAfterAbility { get; private set; }
        [field: SerializeField] public bool NeedPreparationIndicator { get; private set; }
        [field: SerializeField] public float RootMultiplierBeforeAbilityStart { get; private set; } = 1;
        [field: SerializeField] public float RootMultiplierAfterAbilityStart { get; private set; } = 1;
        [field: SerializeField] public LayerMask ObstacleLayers { get; private set; }

#if UNITY_EDITOR

        [Button]
        public void AutomaticCalculatePreparation(AnimationClip animationClip)
        {
            AnimationEvent startPreparationEvent =
                animationClip.events.FirstOrDefault(x =>
                    x.functionName.Equals(AbilityEventNames.PreparationStartEventName));
            AnimationEvent endPreparationEvent =
                animationClip.events.FirstOrDefault(x =>
                    x.functionName.Equals(AbilityEventNames.PreparationEndEventName));

            if (startPreparationEvent == null || endPreparationEvent == null)
                return;

            StartAttackTime = startPreparationEvent.time;
            PreparationAttackTime = endPreparationEvent.time - startPreparationEvent.time;
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}