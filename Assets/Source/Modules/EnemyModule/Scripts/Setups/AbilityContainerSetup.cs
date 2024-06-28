using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Scripts.EditorTools;
using UnityEngine;

namespace Source.Scripts.Setups
{
    [CreateAssetMenu(fileName = "AbilityContainerSetup", menuName = "Setups/AbilityContainerSetup")]
    public class AbilityContainerSetup : SerializedScriptableObject
    {
        [field: SerializeField, InlineEditor]
        public List<AbilitySetup> AbilitySetups { get; private set; } = new List<AbilitySetup>();

#if UNITY_EDITOR
        [Button]
        private void CreateAsset([FolderPath]string path,string name)
        {
            AssetCreator<AbilitySetup> assetCreator = new AssetCreator<AbilitySetup>();
            AbilitySetups.Add(assetCreator.CreateAsset(path, name));
        }
#endif
    }
}