#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Source.Scripts.EditorTools;
using UnityEditor;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.Setups.Tools
{
    public class AbilityCreator
    {
        [Button]
        public T CreateNewAbility<T>([FolderPath] string folder, string nameAbility,AnimationClip animationClip) where T : BaseAbilitySetup, new()
        {
            var assetCreator = new AssetCreator<T>();

            T attackAbilitySetup = assetCreator.CreateAsset(folder, nameAbility);
            attackAbilitySetup.CreateAbilityData(nameAbility + "Data", animationClip,folder);
            attackAbilitySetup.CreateAbilityAction(nameAbility, folder);
            attackAbilitySetup.CreateAbilityAction(nameAbility, folder, BaseAbilitySetup.ActionType.EndPreparation);
            attackAbilitySetup.CreateAbilityAction(nameAbility, folder, BaseAbilitySetup.ActionType.StartAbility);
            attackAbilitySetup.CreateAbilityAction(nameAbility, folder, BaseAbilitySetup.ActionType.EndAbility);
            EditorUtility.SetDirty(attackAbilitySetup);
            AssetDatabase.SaveAssets();
            return attackAbilitySetup;
        }
    }
}
#endif