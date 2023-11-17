using System;
using System.Collections.Generic;
using UnityEngine;
using Visitors;

namespace Tools
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private SwordAttackVisitor _swordAttackVisitor;
        [SerializeField] private Collider _collider;

        private List<HitBox> _attackedHitBoxes = new List<HitBox>();

      
        public void Enable()
        {
            _collider.enabled = true;
            _attackedHitBoxes.Clear();
        }

        public void Disable()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HitBox hitBox) == false) return;

            if (_attackedHitBoxes.Contains(hitBox) == false)
            {
                hitBox.Accept(_swordAttackVisitor);
                _attackedHitBoxes.Add(hitBox);
            }
        }
    }
}