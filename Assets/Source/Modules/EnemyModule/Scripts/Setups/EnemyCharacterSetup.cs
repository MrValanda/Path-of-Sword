using Sirenix.OdinInspector;
using Source.Scripts.EditorTools;
using Source.Scripts.Setups;
using Source.Scripts.Setups.Characters;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.Setups
{
    [CreateAssetMenu(fileName = "EnemyCharacterSetup", menuName = "Setups/Characters/EnemyCharacterSetup")]
    public class EnemyCharacterSetup : SerializedScriptableObject
    {
        [field: SerializeField]
        [field: TabGroup("Stats")]
        public float DefaultHealth { get; private set; }
        
        [field: SerializeField]
        [field: TabGroup("Stats")]
        public float DefaultStamina { get; private set; }

        [field: SerializeField]
        [field: TabGroup("Stats")]
        public float DamageMultiplier { get; private set; }

        [field: SerializeField]
        [field: TabGroup("Stats")]
        public float DefaultMoveSpeed { get; private set; }

        [field: SerializeField]
        [field: TabGroup("Stats")]
        public float AttackRadius { get; private set; }
        
        [field: SerializeField]
        [field: InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [field: TabGroup("Stats")]
        public DamageableContainerSetup AttackedUnits { get; private set; }

        [field: SerializeField]
        [field: InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [field: TabGroup("MoveSet")]
        public MoveSetSetup MoveSetSetup { get; private set; }

        [field: SerializeField]
        [field: InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [field: TabGroup("Ability")]
        public AbilityContainerSetup AbilityContainerSetup { get; private set; }

        [Button]
        public void CreateAbilityContainerSetup(string setupName,
            [FolderPath] string path = @"Assets/Source/Setups/Attacks")
        {
            var assetCreator = new AssetCreator<AbilityContainerSetup>();
            AbilityContainerSetup = assetCreator.CreateAsset(path, setupName);
        }
    }
}