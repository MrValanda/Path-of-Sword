using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    [Serializable]
    public class InputMouseDownCondition : IGameCondition
    {
        [SerializeField] private int _mouseIndex;
        
        public TaskStatus GetConditionStatus()
        {
            return Input.GetMouseButtonDown(_mouseIndex) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}