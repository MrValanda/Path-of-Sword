using System;
using System.Collections.Generic;
using Source.Scripts.CombatModule;
using Source.Scripts.States;
using UnityEngine;

namespace Source.Scripts.EntityDataComponents
{
    [Serializable]
    public class AttackStateComponentData
    {
        [field:SerializeField] public CombatMoveSetSetup CombatMoveSets { get; private set; }
        [field:SerializeField] public Transform WhoWasRotate{ get; private set; }
        [field:SerializeField] public float RotationSpeed{ get; private set; }
        
        public Transform Orientation{ get; private set; }

        public void Initialize(Transform orientation)
        {
            Orientation = orientation;
        }
    }
}
