using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Modules.CombatModule.Scripts;
using Source.Modules.CombatModule.Scripts.Parry;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Modules.CompositeRootModule
{
    public class PlayerCompositeRoot : MonoBehaviour
    {
        [SerializeField] private Entity _playerEntity;
        [SerializeField,TabGroup("AttackState")] private AttackStateComponentData _attackStateComponentData;
        [SerializeField,TabGroup("Equipment")] private Equipment _equipment;
        [SerializeField] private Transform _orientation;
        [SerializeField] private InputMouseDownCondition _inputMouseDownCondition;
        [SerializeField] private ParryHandlerData _parryHandlerData;
     
        public void Compose()
        {
            Container<ICondition> conditionsContainer =
                new Container<ICondition>(new List<ICondition>()
                {
                    _inputMouseDownCondition
                });
            _attackStateComponentData.Initialize(_orientation, conditionsContainer);
            
            _equipment.Initialize();
            
            _playerEntity.Add(_attackStateComponentData);
            _playerEntity.Add(_equipment);
           
            ParryHandler parryHandler = new ParryHandler();
            parryHandler.Initialize(_playerEntity,_parryHandlerData);
            _playerEntity.Add(parryHandler);
        }
    }
}
