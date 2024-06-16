using System.Collections.Generic;

namespace Source.Modules.CombatModule.Scripts
{
    public class AttackStateDataContainer
    {
        private Dictionary<MoveSetType, AttackStateComponentData> _attackStateComponentDatas;

        public AttackStateDataContainer(Dictionary<MoveSetType, AttackStateComponentData> attackStateComponentDatas)
        {
            _attackStateComponentDatas = attackStateComponentDatas;
        }

        public AttackStateComponentData this[MoveSetType moveSetType] => _attackStateComponentDatas[moveSetType];

    }
}