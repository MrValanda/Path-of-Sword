using System;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    [Serializable]
    public class InputMouseDownCondition : ICondition
    {
        [SerializeField] private int _mouseIndex;
        
        public bool GetStatus()
        {
            return Input.GetMouseButtonDown(_mouseIndex);
        }
    }
}