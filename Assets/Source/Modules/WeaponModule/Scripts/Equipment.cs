using System;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.Scripts.EntityLogic;
using Source.Scripts.Visitors;
using Source.Scripts.WeaponModule;
using UnityEngine;

namespace Source.Modules.WeaponModule.Scripts
{
    [Serializable]
    public class Equipment 
    {
        [SerializeField] private Entity _ownerEntity;
        [SerializeField] private Transform _orientation;
        [SerializeField] private WeaponData _defaultWeaponData;
        [SerializeField] private Transform _weaponLocator;
        [SerializeField] private SwordAttackVisitor _swordAttackVisitor;
        public WeaponData CurrentWeaponData { get; private set; }

        public void Initialize()
        {
            WeaponEntity currentWeaponEntity = LeanPool.Spawn(_defaultWeaponData.weaponEntity, _weaponLocator);
            EquipWeapon(currentWeaponEntity);
        }
        
        public void EquipWeapon(WeaponEntity currentWeaponEntity)
        {
            CurrentWeaponData = new WeaponData() {weaponEntity = currentWeaponEntity};

            if (currentWeaponEntity.TryGet(out Weapon weapon))
            {
                weapon.Initialize(_swordAttackVisitor, _orientation);
                _ownerEntity.Add(weapon);
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