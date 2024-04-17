using Source.Scripts.GameActions;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.AttackPointCalculators
{
    public class RangeAttackPointCalculator : IAttackPointCalculator
    {
        private readonly CalculatorNavMeshPath _calculatorNavMeshPath;
        private readonly Transform _target;
        private readonly Transform _sourceTransform;
        private Vector3 _previousPoint;

        public RangeAttackPointCalculator(Transform target, Transform sourceTransform)
        {
            _calculatorNavMeshPath = new CalculatorNavMeshPath();
            _target = target;
            _sourceTransform = sourceTransform;
        }

        public Vector3 GetDirection()
        {
            return _calculatorNavMeshPath.GetDirectionToNextPoint(_sourceTransform.position, _target.position);
        }
    }
}