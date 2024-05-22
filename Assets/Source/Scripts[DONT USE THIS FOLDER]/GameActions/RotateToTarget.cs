using System;
using BehaviorDesigner.Runtime.Tasks;
using Cinemachine.Utility;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.DamageableFindersModule;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class RotateToTarget : IGameAction
    {
        private Entity _entity;
        private float _maxRadiansDeltaAngle;

        public RotateToTarget(Entity entity, float maxRadiansDeltaAngle)
        {
            _entity = entity;
            _maxRadiansDeltaAngle = maxRadiansDeltaAngle;
        }

        public TaskStatus ExecuteAction()
        {
            Vector3 currentDirection = _entity.transform.forward;
            Vector3 desiredDirection = _entity.Get<DamageableSelector>().SelectedDamageable.transform.position -
                                       _entity.transform.position;
            float rotationSpeed = _maxRadiansDeltaAngle * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(currentDirection, desiredDirection, rotationSpeed, 0f);
            Quaternion transformRotation = Quaternion.LookRotation(newDirection);
            transformRotation.x = transformRotation.z = 0;
            _entity.transform.rotation = transformRotation;
            if (Vector3.Dot(desiredDirection.normalized, _entity.transform.forward) < 0.95f) return TaskStatus.Running;
            return TaskStatus.Success;
        }
    }
}