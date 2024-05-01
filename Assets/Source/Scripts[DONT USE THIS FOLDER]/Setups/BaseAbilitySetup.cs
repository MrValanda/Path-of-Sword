using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.Scripts.AnimationEventNames;
using Source.Scripts.EditorTools;
using UnityEngine;

namespace Source.Scripts.Setups
{
    public class BaseAbilitySetup : SerializedScriptableObject
    {
        [field: SerializeField] public AnimationClip AbilityAnimation { get; private set; }

        [field: SerializeField, InlineEditor(InlineEditorModes.FullEditor)]
        public AbilityDataSetup AbilityDataSetup { get; private set; }

        [field: SerializeField, InlineEditor] public AbilityConditionSetup AbilityToUseConditions { get; private set; }

        [field: SerializeField, InlineEditor] public AbilityActionSetup AbilityStartedActions { get; private set; }

        [field: SerializeField, InlineEditor] public AbilityActionSetup AbilityEndedActions { get; private set; }

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
        private void CreateAbilityDataSetup(string name, string path = @"Assets/Source/Setups/Abilities/AbilityData")
        {
            AssetCreator<AbilityDataSetup> assetCreator = new();
            AbilityDataSetup ??= assetCreator.CreateAsset(path, name);
        }
#endif
    }
}