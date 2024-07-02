using UnityEngine;

namespace Source.Scripts.Interfaces
{
    public interface IMovement
    {
        public void Init(float speed);
        public void Move(Vector3 direction);
    }
}