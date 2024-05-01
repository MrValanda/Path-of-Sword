using Source.Scripts.EntityDataComponents;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Scripts.PlayerModule
{
    public class PlayerCompositeRoot : MonoBehaviour
    {
        [SerializeField] private Entity _playerEntity;
        [SerializeField] private AttackStateComponentData _attackStateComponentData;
        [SerializeField] private Transform _orientation;
        public void Compose()
        {
            _attackStateComponentData.Initialize(_orientation);
            _playerEntity.Add(_attackStateComponentData);
        }
    }
}
