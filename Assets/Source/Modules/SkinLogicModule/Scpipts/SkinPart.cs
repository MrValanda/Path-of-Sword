using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkinLogic
{
    [CreateAssetMenu(fileName = "SkinPart", menuName = "Setups/Skins/SkinPart", order = 0)]
    public class SkinPart : SerializedScriptableObject
    {
        [field: SerializeField] public string SkinPartName { get; private set; }
        [field: SerializeField] public string TitleText { get; private set; }
        [field: SerializeField] public SkinPartType SkinPartType { get; private set; }
        [field: SerializeField,InlineEditor(InlineEditorModes.LargePreview)] public GameObject SkinModel { get; private set; }
        [field: SerializeField] public SkinEquipmentData SkinEquipmentData { get; private set; }

        private void OnValidate()
        {
            SkinPartName = name;
        }
    }

    [Serializable]
    public class SkinEquipmentData
    {
        [field: SerializeField] public SkinRarityType SkinRarityType { get; private set; }
        
        [field: SerializeField] public Sprite IconSkin { get; private set; }
        [field: SerializeField] public int HealthValue { get; private set; } = 1;
     
        [field: SerializeField] public int DamageValue { get; private set; } = 1;
        [field: SerializeField] public float UpgradeValueByLvl { get; private set; }
        [field: SerializeField] public float Cost { get; private set; }
        [field: SerializeField] public EquipmentBuffType EquipmentBuffType { get; private set; }

    }

    public enum EquipmentBuffType
    {
        None = 0,
        Attack = 1,
        Health = 2,
    }
    
    public enum SkinPartType
    {
        None = 0,
        Head = 1,
        Body = 2,
        Weapon = 3,
        Cap = 4,
        Personality = 5,
        Cloak = 6,
        Skin = 7,
        Legs = 8
    }

    public enum SkinRarityType
    {
        none = 0,
        
        common = 1,
        rare = 2,
        epic = 3,
        legendary = 4,
    }
}