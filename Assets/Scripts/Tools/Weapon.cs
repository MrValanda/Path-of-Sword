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


      
        public void Enable()
        {
            _collider.enabled = true;
        }

        public void Disable()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HitBox hitBox) == false) return;

            hitBox.Accept(_swordAttackVisitor);
           
        }
    }
}