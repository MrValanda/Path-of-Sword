using UnityEngine;

namespace Source.Scripts.Enemy
{
    public class AttackRadiusTrigger : MonoBehaviour
    {
        private Collider _attackRadiusCollider;

        private void Start()
        {
            _attackRadiusCollider = GetComponent<Collider>();
        }

        public void Disable()
        {
            _attackRadiusCollider.enabled = false;
            enabled = false;
        }
    }
}