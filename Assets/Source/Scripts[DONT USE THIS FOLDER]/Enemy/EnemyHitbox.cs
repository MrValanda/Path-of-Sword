using Sirenix.OdinInspector;
using Source.Scripts.InterfaceLinker;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemy
{
    public class EnemyHitbox : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField,ShowIf("@_enemy == null")] private DamageableLinker _damageableLinker;
        [field: SerializeField] public Transform ParticlePoint { get; private set; }
        [SerializeField] private bool _needHitbox = true;
        public void Init()
        {
            if (_needHitbox == false)
            {
                Disable();
            }

            if(_enemy != null)
            {
            }
        }

        public void TakeDamage(double damage)
        {
            if(_enemy != null)
            {
            //    _enemy.TakeDamage(damage);
            }
            else
            {
             
            }
        }

        public void Enable()
        {
            if (_needHitbox == false) return;
            
            // gameObject.layer = LayerMask.NameToLayer("Enemy");
            // enabled = true;
        }

        private void OnEnemyDead(IDying obj)
        {
            if(_enemy != null)
            {
                obj.Dead -= OnEnemyDead;
                Disable();
            }
        }

        public void Disable()
        {
            // gameObject.layer = LayerMask.NameToLayer("PlayerNotCollision");
            // enabled = false;
        }
    }
}