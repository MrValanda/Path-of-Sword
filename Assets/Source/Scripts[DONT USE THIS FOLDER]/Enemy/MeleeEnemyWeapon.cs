using System;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemy
{
    public class MeleeEnemyWeapon : EnemyWeapon
    {
        [SerializeField] private ParticleSystem _trail;
        
        public override void OnEnemyWeaponEnable()
        {
            _trail.Play();
        }

        public override void OnEnemyWeaponDisable()
        {
            _trail.Stop();
        }
    }
}