using Sirenix.OdinInspector;
using Source.Scripts.Enemy;
using UnityEngine;

namespace Source.Scripts.Setups.Characters
{
    [CreateAssetMenu(fileName = "EnemyCharacterSetup", menuName = "Setups/Characters/EnemyCharacterSetup")]
    public class EnemyCharacterSetup : SerializedScriptableObject
    {
        [field: SerializeField] public EnemyTaskType EnemyTaskType { get; private set; }
        [field: SerializeField] public double DefaultHealth { get; private set; }
        [field: SerializeField] public double HealthMultiplier { get; private set; }
        [field: SerializeField] public double DamageMultiplier { get; private set; }
        [field: SerializeField] public float DefaultMoveSpeed { get; private set; }
        [field: SerializeField] public float DefaultAcceleration { get; private set; }
        [field: SerializeField] public float AttackRadius { get; private set; }
        [field: SerializeField] public float DetectRadius { get; private set; }
        [field: SerializeField, Range(0, 360)] public float ViewAngle { get; private set; } = 360;
        [field: SerializeField] public float ForcePush { get; private set; }
        [field: SerializeField] public float SlowMotionTime { get; private set; }
        [field: SerializeField] public float MaxChaseDistance { get; private set; }
        [field: SerializeField, InlineEditor] public DamageableContainerSetup AttakedUnits { get; private set; }
        [field: SerializeField, InlineEditor] public MoveSetSetup MoveSetSetup { get; private set; }

        [field: SerializeField, InlineEditor(InlineEditorModes.FullEditor)]
        public EnemyWeapon EnemyWeaponLeftHand { get; private set; }
        
        [field: SerializeField, InlineEditor(InlineEditorModes.FullEditor)]
        public EnemyWeapon EnemyWeaponRightHand { get; private set; }

        [field: SerializeField, InlineEditor] public AbilityContainerSetup AbilityContainerSetup { get; private set; }
        [field: SerializeField, InlineEditor] public DropListSetup DropListSetup { get; private set; }
        [field: SerializeField, InlineEditor] public DropListSetup WhenTakeDamageDropListSetup { get; private set; }
    }

    public enum EnemyTaskType
    {
        Default = 0,
        Boss = 1,
    }
}