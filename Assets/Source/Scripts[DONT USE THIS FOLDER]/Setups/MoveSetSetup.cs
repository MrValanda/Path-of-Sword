using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif

using Source.Scripts.EditorTools;
using UnityEngine;

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
#endif
    }

    [Serializable]
    public class AbilityChain
    {
        [field: SerializeField, InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        public List<AttackAbilitySetup> AbilitySetups { get; private set; }

        [field: SerializeField] public float AfkTimeAfterChain { get; private set; } = 0;

#if UNITY_EDITOR
        [Button]
        private void CreateAbilityDataSetup(string name, [FolderPath] string path = @"Assets/Source/Setups/Attacks")
        {
            AssetCreator<AttackAbilitySetup> assetCreator = new AssetCreator<AttackAbilitySetup>();
            AttackAbilitySetup attackAbilitySetup = assetCreator.CreateAsset(path, name);
            attackAbilitySetup.CreateAbilityData(name + "DataSetup", path);
            AbilitySetups.Add(attackAbilitySetup);
        }
#endif
    }
}