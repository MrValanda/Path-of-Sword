using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.InterfaceLinker;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameConditionals
{
    [Serializable]
    public class IsDeathCondition : IGameCondition
    {
        [SerializeField] private DyingLinker _dying;

        public IsDeathCondition(DyingLinker dying)
        {
            _dying = dying;
        }

        public TaskStatus GetConditionStatus()
        {
            return _dying.Value.IsDead ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}