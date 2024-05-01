using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Source.Scripts.BehaviorsNodes.SharedVariables;
using Source.Scripts.Enemy;
using UnityEngine;

namespace Source.Scripts.BehaviorsNodes.Actions
{
    public class SetChangeCurrentPoint : Action
    {
        public SharedPatrolPointList Points;
        public SharedTransform CurrentPoint;

        private int _lastIndex;
        private float _timeToGetNextPoint;
        private float _lastTimeReachedPosition;
        
        public override void OnStart()
        {
            _lastTimeReachedPosition = Time.time;
            _timeToGetNextPoint = Points.Value[_lastIndex].TimeToGetNextPoint;
        }

        public override TaskStatus OnUpdate()
        {
            if (Time.time - _lastTimeReachedPosition <= _timeToGetNextPoint) 
            {
                return TaskStatus.Running;
            }

            _lastIndex = (_lastIndex + 1) % Points.Value.Count;
            PatrolPointData patrolPointData = Points.Value[_lastIndex];
            CurrentPoint.Value = patrolPointData.PatrolPoint;
            return TaskStatus.Success;
        }
    }
}