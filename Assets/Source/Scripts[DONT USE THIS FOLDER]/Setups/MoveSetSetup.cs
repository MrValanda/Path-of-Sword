using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Scripts.EditorTools;
using UnityEngine;

namespace Source.Scripts.Setups
{
    [CreateAssetMenu(fileName = "MoveSetSetup", menuName = "Setups/MoveSetSetup")]
    public class MoveSetSetup : ScriptableObject
    {
        [field: SerializeField]
        public List<AbilityChain> AbilityChains { get; private set; }
    }
    
    [Serializable]
    public class AbilityChain
    {
        [field: SerializeField, InlineEditor(InlineEditorModes.FullEditor)]
        public List<AttackAbilitySetup> AbilitySetups { get; private set; }

        [field: SerializeField] public float AfkTimeAfterChain { get; private set; } = 0;

#if UNITY_EDITOR
        [Button]
        private void CreateAbilityDataSetup(string name, string path = @"Assets/Source/Setups/Attacks")
        {
            AssetCreator<AttackAbilitySetup> assetCreator = new AssetCreator<AttackAbilitySetup>();
            AbilitySetups.Add(assetCreator.CreateAsset(path, name));
        }
#endif
    }
}