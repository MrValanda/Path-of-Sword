using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Scripts.Utils.SO;
using UnityEditor;
using UnityEngine;

namespace Source.Scripts.SkinLogic
{
    [CreateAssetMenu(fileName = "SkinEffectsContainer", menuName = "Setups/Skins/AllSkinPartsContainer", order = 0)]
    public class AllSkinPartsContainer : LoadableScriptableObject<AllSkinPartsContainer>
    {
        [field: SerializeField] public List<SkinPart> AllSkinParts;

#if UNITY_EDITOR
        [Button]
        private void LoadAll()
        {
            AllSkinParts.Clear();
            string[] materialGUIDs = AssetDatabase.FindAssets("t:SkinPart");
            foreach (string guid in materialGUIDs)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SkinPart skinPart = AssetDatabase.LoadAssetAtPath<SkinPart>(path);
                AllSkinParts.Add(skinPart);
            }
        }
#endif
    }
}