using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.DamageableFindersModule;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using UnityEngine;

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