using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.Scripts.AnimationEventNames;
using Source.Scripts.EditorTools;
using Source.Scripts.Setups;
using UnityEditor;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.Setups
{
    public class BaseAbilitySetup : SerializedScriptableObject
    {
        [field: SerializeField]
        [field: InlineEditor(InlineEditorModes.LargePreview)]
        public AnimationClip AbilityAnimation { get; private set; }

        [field: SerializeField]
        [field: InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [field: TabGroup("AbilityDataSetup")]
        public AbilityDataSetup AbilityDataSetup { get; private set; }

        [field: SerializeField]
        [field: InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [field: TabGroup("Conditions")]
        public AbilityConditionSetup AbilityToUseConditions { get; private set; }

        [field: SerializeField]
        [field: InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [field: TabGroup("AbilityPreparationStartActions")]
        public AbilityActionSetup AbilityPreparationStartActions { get; private set; }

        [field: SerializeField]
        [field: InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [field: TabGroup("AbilityPreparationEndActions")]
        public AbilityActionSetup AbilityPreparationEndActions { get; private set; }

        [field: SerializeField]
        [field: InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [field: TabGroup("StartAbilityActions")]
        public AbilityActionSetup AbilityStartedActions { get; private set; }

        [field: SerializeField]
        [field: InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [field: TabGroup("EndAbilityActions")]
        public AbilityActionSetup AbilityEndedActions { get; private set; }


        private void OnValidate()
        {
            if (AbilityAnimation == null) return;

            var functionsName = AbilityAnimation.events.Select(x => x.functionName).ToList();
            var desiredEventNames = new List<string>
            {
                AbilityEventNames.AbilityEndEventName,
                AbilityEventNames.AbilityStartEventName,
                AbilityEventNames.PreparationEndEventName,
                AbilityEventNames.PreparationStartEventName
            };

            foreach (var desiredEventName in desiredEventNames)
                if (functionsName.Contains(desiredEventName) == false)
                {
                    AbilityAnimation = null;
                    Debug.LogError("It is not Ability animation");
                }
        }

#if UNITY_EDITOR
        [Button]
        public void CreateAbilityData(string abilityDataName, AnimationClip animationClip,
            [FolderPath] string path = @"Assets/Source/Setups/Attacks")
        {
            var assetCreator = new AssetCreator<AbilityDataSetup>();

            AbilityDataSetup = assetCreator.CreateAsset(path, abilityDataName);
            AbilityDataSetup.AutomaticCalculatePreparation(animationClip);
            AbilityAnimation = animationClip;
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        [Button]
        public void CreateAbilityAction(string abilityActionName,
            [FolderPath] string path = @"Assets/Source/Setups/Attacks",
            ActionType actionType = ActionType.StartPreparation)
        {
            var assetCreator = new AssetCreator<AbilityActionSetup>();
            AbilityActionSetup abilityActionSetup = assetCreator.CreateAsset(path, abilityActionName + actionType);
            SetDataByType(actionType, abilityActionSetup);
        }

        [Button]
        [TabGroup("Conditions")]
        public void CreateAbilityCondition(string abilityActionName,
            [FolderPath] string path = @"Assets/Source/Setups/Attacks")
        {
            var assetCreator = new AssetCreator<AbilityConditionSetup>();
            AbilityConditionSetup abilityConditionSetup = assetCreator.CreateAsset(path, abilityActionName);
        }

        private void SetDataByType(ActionType actionType, AbilityActionSetup attackAbilitySetup)
        {
            switch (actionType)
            {
                case ActionType.StartPreparation:
                    AbilityPreparationStartActions = attackAbilitySetup;
                    break;
                case ActionType.EndPreparation:
                    AbilityPreparationEndActions = attackAbilitySetup;
                    break;
                case ActionType.StartAbility:
                    AbilityStartedActions = attackAbilitySetup;
                    break;
                case ActionType.EndAbility:
                    AbilityEndedActions = attackAbilitySetup;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }
        }

        public enum ActionType
        {
            StartPreparation = 0,
            EndPreparation = 1,
            StartAbility = 2,
            EndAbility = 3
        }
#endif
    }
}