using System.Collections.Generic;
using Lean.Pool;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.ProjectilesLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class DamagedProjectileLogic : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMaskToDestroyProjectile;
        [SerializeField] private ParticleSystem _destroyEffect;
        protected List<string> AttackableTypes;

        private float _damage;
        private float _projectileSpeed;
        private float _maxDistance;
        private Rigidbody _rigidbody;
        private Vector3 _velocity;
        private Vector3 _startPoint;
        private Transform _target;


        public void Init(float damage, List<string> attackableTypes, float maxDistance, Transform target,
            float projectileSpeed, LayerMask layerMaskToDestroyProjectile)
        {
            Init(damage, attackableTypes, maxDistance, target, projectileSpeed);
            _layerMaskToDestroyProjectile = layerMaskToDestroyProjectile;
        }

        public void Init(float damage, List<string> attackableTypes, float maxDistance, Transform target,
            float projectileSpeed)
        {
            _damage = damage;
            AttackableTypes = attackableTypes;
            _maxDistance = maxDistance;
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _startPoint = transform.position;
            _target = target;
            _projectileSpeed = projectileSpeed;
        }

        private void FixedUpdate()
        {
            if (_target == null)
            {
                _rigidbody.velocity = _velocity;
            }
            else
            {
                _rigidbody.velocity = ((_target.position + Vector3.up * 2f) - transform.position).normalized *
                                      _projectileSpeed;
            }

            if (Vector3.Distance(_startPoint, transform.position) >= _maxDistance)
            {
                LeanPool.Despawn(gameObject);
            }
        }

        public void Move(Vector3 velocity)
        {
            _velocity = velocity;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) &&
                (AttackableTypes == null || AttackableTypes.Contains(damageable.GetType().Name)))
            {
                damageable.ApplyDamage(_damage);
            }

            // if (_layerMaskToDestroyProjectile.Contains(other.gameObject.layer))
            {
                LeanPool.Despawn(gameObject);
            }
        }

        private void OnDisable()
        {
            Vector3 spawnPosition = _target == null ? transform.position : _target.position + Vector3.up * 2;
            LeanPool.Spawn(_destroyEffect, spawnPosition, Quaternion.identity);
        }
    }
}