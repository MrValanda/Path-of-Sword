using Sirenix.OdinInspector;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.EntityDataComponents;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.PlayerModule
{
    public class PlayerCompositeRoot : MonoBehaviour
    {
        [SerializeField] private Entity _playerEntity;
        [SerializeField,TabGroup("AttackState")] private AttackStateComponentData _attackStateComponentData;
        [SerializeField,TabGroup("Equipment")] private Equipment _equipment;
        [SerializeField] private Transform _orientation;
        public void Compose()
        {
            _attackStateComponentData.Initialize(_orientation);
            _equipment.Initialize();
            
            _playerEntity.Add(_attackStateComponentData);
            _playerEntity.Add(_equipment);
        }
    }
}
