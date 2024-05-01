using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.CodeLibrary.ServiceBootstrap;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.GameConditionals
{
    [Serializable]
    public class PlayerIsNearbyCondition : IGameCondition
    {
        [SerializeField] private Transform _basePoint;
        [SerializeField] private float _desiredDistance;

        private Transform _player;

        public void InitData()
        {
            ServiceLocator.Global.Get(out _player);
        }

        public TaskStatus GetConditionStatus()
        {
            return Vector3.Distance(_basePoint.position, _player.transform.position) <= _desiredDistance
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}