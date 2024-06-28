using System;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.Transitions
{
    [Serializable]
    public class EntityContainsType<T> : Transition where T: class 
    {
        [SerializeField] protected Entity _entity;
        public override void OnEnable()
        {
            _entity.ComponentAdded += OnComponentAdded;
            CheckEntity();
        }

        public override void OnDisable()
        {
            _entity.ComponentAdded -= OnComponentAdded;
        }

        private void OnComponentAdded(Type obj)
        {
            CheckEntity();
        }

        private void CheckEntity()
        {
            if (_entity.TryGet(out T _))
            {
                OnNeedTransit(this);
            }
        }
    }
}