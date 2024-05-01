using System;
using UnityEngine;

namespace Source.Scripts.AnimationEventListeners
{
    [RequireComponent(typeof(Animator))]
    public class ShootAnimationEventListener : MonoBehaviour
    {
        public event Action Shooting;
        public event Action ShootingEnded;

        private void Shoot()
        {
            Shooting?.Invoke();
        }
        
        private void ShootEnd()
        {
            ShootingEnded?.Invoke();
        }
    }
    
}