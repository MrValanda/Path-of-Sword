using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Modules.EnemyModule.Scripts.Setups;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using Source.Modules.EnemyModule.Scripts.Setups.Tools;
#endif

namespace Source.Scripts.Setups
{
    [CreateAssetMenu(fileName = "MoveSetSetup", menuName = "Setups/MoveSetSetup")]
    public class MoveSetSetup : ScriptableObject
    {
        [field: SerializeField]
#if UNITY_EDITOR
        [ListDrawerSettings(OnBeginListElementGUI = nameof(BeginDrawListElement),
            OnEndListElementGUI = nameof(EndDrawListElement))]
#endif

        public List<AbilityChain> AbilityChains { get; private set; }
#if UNITY_EDITOR
        private void BeginDrawListElement(int index)
        {
            SirenixEditorGUI.BeginBox($"Chain {index}");
        }

        private void EndDrawListElement(int index)
        {
            SirenixEditorGUI.EndBox();
        }

        [Button]
        public void CreateAbilityDataSetup(string nameAbility,AnimationClip animationClip,
            [FolderPath] string path = @"Assets/Source/Setups/Attacks")
        {
            AbilityCreator abilityCreator = new();
            abilityCreator.CreateNewAbility<AttackAbilitySetup>(path + $"/{nameAbility}", nameAbility,animationClip);
        }
#endif
    }

    [Serializable]
    public class AbilityChain
    {
        [field: SerializeField]
        [field: InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        public List<BaseAbilitySetup> AbilitySetups { get; private set; }

        [field: SerializeField] public float AfkTimeAfterChain { get; private set; }
    }
}