using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.CodeLibrary.ServiceBootstrap;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameConditionals
{
    [Serializable]
    public class TargetIsNearbyCondition : IGameCondition
    {
        [SerializeField] private Transform _basePoint;
        [SerializeField] private Transform _target;
        [SerializeField] private float _desiredDistance;
        
        public TaskStatus GetConditionStatus()
        {
            return Vector3.Distance(_basePoint.position, _target.transform.position) <= _desiredDistance
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}