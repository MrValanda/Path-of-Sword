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
        [SerializeField] private bool _isPressed;
        
        public TaskStatus GetConditionStatus()
        {
            bool mouseButtonDown = _isPressed == false
                ? Input.GetMouseButtonDown(_mouseIndex)
                : Input.GetMouseButton(_mouseIndex);
            return mouseButtonDown ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}