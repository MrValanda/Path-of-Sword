using Source.Scripts;
using UnityEngine;

namespace States
{
    public class RotateToTarget : State
    {
        [SerializeField] private Transform _whoWasRotate;
        [SerializeField] private Transform _target;
        
        protected override void OnUpdate()
        {
            if (_target!= null)
            {
                Vector3 direction = _target.position - _whoWasRotate.position;
                direction.y = 0;
                _whoWasRotate.forward = direction;
            }
        }
    }
}