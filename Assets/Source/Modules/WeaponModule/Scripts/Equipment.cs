using System;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.Modules.CombatModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.WeaponModule;
using UnityEngine;

namespace Source.Modules.WeaponModule.Scripts
{
    [Serializable]
    public class Equipment 
    {
        [SerializeField] private WeaponData _defaultWeaponData;
        [SerializeField] private SwordAttackVisitor _swordAttackVisitor;
        public WeaponData CurrentWeaponData { get; private set; }

        private Entity _ownerEntity;
        private Transform _orientation;

        public void Initialize(Entity ownerEntity)
        {
            _ownerEntity = ownerEntity;
            _orientation = ownerEntity.transform;
            _swordAttackVisitor.Initialize(ownerEntity);
            WeaponEntity currentWeaponEntity = LeanPool.Spawn(_defaultWeaponData.weaponEntity,
                _ownerEntity.Get<WeaponLocator>().transform);
            EquipWeapon(currentWeaponEntity);
        }
        
        public void EquipWeapon(WeaponEntity currentWeaponEntity)
        {
            CurrentWeaponData = new WeaponData() {weaponEntity = currentWeaponEntity};

            if (currentWeaponEntity.TryGet(out Weapon weapon))
            {
                weapon.Initialize(_swordAttackVisitor, _orientation);
                _ownerEntity.Add(weapon);
                Debug.LogError("EquipWeapon " + _ownerEntity.name);
            }

            if (currentWeaponEntity.TryGet(out WeaponTrailContainer weaponTrailContainer))
            {
                _ownerEntity.Add(weaponTrailContainer);
            }
        }

        [Serializable]
        public struct WeaponData
        {
            [InlineEditor(InlineEditorModes.LargePreview)]
            public WeaponEntity weaponEntity;
        }
    }
}