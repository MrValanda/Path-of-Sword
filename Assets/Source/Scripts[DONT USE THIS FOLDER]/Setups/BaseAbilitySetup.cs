using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Source.Scripts.AnimationEventNames;
using Source.Scripts.EditorTools;
using UnityEngine;

namespace Source.Scripts.Setups
{
    public class BaseAbilitySetup : SerializedScriptableObject
    {
        [field: SerializeField,InlineEditor(InlineEditorModes.LargePreview)] public AnimationClip AbilityAnimation { get; private set; }

        [field: SerializeField, InlineEditor(InlineEditorObjectFieldModes.Foldout), TabGroup("AbilityDataSetup")]
        public AbilityDataSetup AbilityDataSetup { get; private set; }

        [field: SerializeField, InlineEditor(InlineEditorObjectFieldModes.Foldout), TabGroup("Conditions")]
        public AbilityConditionSetup AbilityToUseConditions { get; private set; }

        [field: SerializeField, InlineEditor(InlineEditorObjectFieldModes.Foldout),
                TabGroup("AbilityPreparationStartActions")]
        public AbilityActionSetup AbilityPreparationStartActions { get; private set; }

        [field: SerializeField, InlineEditor(InlineEditorObjectFieldModes.Foldout),
                TabGroup("AbilityPreparationEndActions")]
        public AbilityActionSetup AbilityPreparationEndActions { get; private set; }

        [field: SerializeField, InlineEditor(InlineEditorObjectFieldModes.Foldout), TabGroup("StartAbilityActions")]
        public AbilityActionSetup AbilityStartedActions { get; private set; }

        [field: SerializeField, InlineEditor(InlineEditorObjectFieldModes.Foldout), TabGroup("EndAbilityActions")]
        public AbilityActionSetup AbilityEndedActions { get; private set; }


        private void OnValidate()
        {
            if (AbilityAnimation == null) return;

            List<string> functionsName = AbilityAnimation.events.Select(x => x.functionName).ToList();
            List<string> desiredEventNames = new List<string>()
            {
                AbilityEventNames.AbilityEndEventName,
                AbilityEventNames.AbilityStartEventName,
                AbilityEventNames.PreparationEndEventName,
                AbilityEventNames.PreparationStartEventName,
            };

            foreach (string desiredEventName in desiredEventNames)
            {
                if (functionsName.Contains(desiredEventName) == false)
                {
                    AbilityAnimation = null;
                    Debug.LogError("It is not Ability animation");
                }
            }
        }

#if UNITY_EDITOR
        [Button]
        public void CreateAbilityData(string abilityDataName,
            [FolderPath] string path = @"Assets/Source/Setups/Attacks")
        {
            AssetCreator<AbilityDataSetup> assetCreator = new AssetCreator<AbilityDataSetup>();

            AbilityDataSetup = assetCreator.CreateAsset(path, abilityDataName);
        }

        [Button]
        public void CreateAbilityAction(string abilityActionName,
            [FolderPath] string path = @"Assets/Source/Setups/Attacks")
        {
            AssetCreator<AbilityActionSetup> assetCreator = new AssetCreator<AbilityActionSetup>();
            assetCreator.CreateAsset(path, abilityActionName);
        }
#endif
    }
}