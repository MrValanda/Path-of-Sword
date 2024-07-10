using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Scripts.Setups;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
using Source.Modules.EnemyModule.Scripts.Setups.Tools;
#endif

namespace Source.Modules.EnemyModule.Scripts.Setups
{
    [CreateAssetMenu(fileName = "AbilityContainerSetup", menuName = "Setups/AbilityContainerSetup")]
    public class AbilityContainerSetup : SerializedScriptableObject
    {
        [field: SerializeField]
        [field: InlineEditor]
        public List<AbilitySetup> AbilitySetups { get; private set; } = new();

#if UNITY_EDITOR
        
        [Button]
        private void CreateAsset(string nameAbility, AnimationClip animationClip,
            [FolderPath] string path = "Assets/Source/Setups/EnemySetups")
        {
            AbilityCreator abilityCreator = new();
            AbilitySetups.Add(
                abilityCreator.CreateNewAbility<AbilitySetup>(path + $"/{nameAbility}", nameAbility, animationClip));
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}