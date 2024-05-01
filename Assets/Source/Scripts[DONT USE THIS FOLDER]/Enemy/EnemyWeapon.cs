using System.Collections.Generic;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemy
{
    public class EnemyWeapon : MonoBehaviour
    {

        [field: SerializeField] public Collider Collider { get; private set; }
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        
        protected List<string> _attackableTypes;
        protected bool _isInitialized;
        
        public void Init(List<string> attackableTypes)
        {
            _attackableTypes = attackableTypes;
            Collider.isTrigger = true;
            _isInitialized = true;
            Rigidbody.isKinematic = true;
            Disable();
        }
        
        public void Show()
        {
            enabled = true;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            enabled = false;
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            OnEnemyWeaponEnable();
            Collider.enabled = true;
        }

        public void Disable()
        {
            OnEnemyWeaponDisable();
            Collider.enabled = false;
        }
        
        public virtual void OnEnemyWeaponDisable(){}
        public virtual void OnEnemyWeaponEnable(){}
    }
}