using Sirenix.OdinInspector;
using Source.Scripts.EditorTools;
using UnityEngine;

namespace Source.Scripts.Setups.Characters
{
    [CreateAssetMenu(fileName = "EnemyCharacterSetup", menuName = "Setups/Characters/EnemyCharacterSetup")]
    public class EnemyCharacterSetup : SerializedScriptableObject
    {
        [field: SerializeField,TabGroup("Stats")] public float DefaultHealth { get; private set; }
        [field: SerializeField,TabGroup("Stats")] public float HealthMultiplier { get; private set; }
        [field: SerializeField,TabGroup("Stats")] public float DamageMultiplier { get; private set; }
        [field: SerializeField,TabGroup("Stats")] public float DefaultMoveSpeed { get; private set; }
        [field: SerializeField,TabGroup("Stats")] public float DefaultAcceleration { get; private set; }
        [field: SerializeField,TabGroup("Stats")] public float AttackRadius { get; private set; }
        [field: SerializeField,TabGroup("Stats")] public float DetectRadius { get; private set; }
        [field: SerializeField, Range(0, 360),TabGroup("Stats")] public float ViewAngle { get; private set; } = 360;
        [field: SerializeField,  InlineEditor(InlineEditorObjectFieldModes.Foldout),TabGroup("Stats")] public DamageableContainerSetup AttakedUnits { get; private set; }
        [field: SerializeField,  InlineEditor(InlineEditorObjectFieldModes.Foldout),TabGroup("MoveSet")] public MoveSetSetup MoveSetSetup { get; private set; }

        [field: SerializeField,  InlineEditor(InlineEditorObjectFieldModes.Foldout),TabGroup("Ability")] public AbilityContainerSetup AbilityContainerSetup { get; private set; }
        [field: SerializeField,  InlineEditor(InlineEditorObjectFieldModes.Foldout),TabGroup("Drop")] public DropListSetup DropListSetup { get; private set; }
        [field: SerializeField,  InlineEditor(InlineEditorObjectFieldModes.Foldout),TabGroup("Drop")] public DropListSetup WhenTakeDamageDropListSetup { get; private set; }

        [Button]
        public void CreateAbilityContainerSetup(string setupName,[FolderPath]string path =@"Assets/Source/Setups/Attacks")
        {
            AssetCreator<AbilityContainerSetup> assetCreator = new AssetCreator<AbilityContainerSetup>();
            AbilityContainerSetup = assetCreator.CreateAsset(path, setupName);
        }
    }

    public enum EnemyTaskType
    {
        Default = 0,
        Boss = 1,
    }
}