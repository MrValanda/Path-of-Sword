using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.DamageableFindersModule;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.IGameConditions
{
    public class DamageableSelectorIsNotNull : IGameCondition
    {
        private readonly Entity _entity;

        public DamageableSelectorIsNotNull(Entity entity)
        {
            _entity = entity;
        
            if (_entity.Contains<DamageableSelector>() == false)
            {
                Debug.LogError($"{_entity.name} not contains {nameof(DamageableSelector)}");
            }
        }

        public TaskStatus GetConditionStatus()
        {
            return _entity.Get<DamageableSelector>().SelectedDamageable == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}