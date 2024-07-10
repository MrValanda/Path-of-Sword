using System;
using Source.Modules.Tools;
using Source.Scripts.Utils;
using UnityEngine;

namespace Source.Scripts.EntityLogic
{
    
    [RequireComponent(typeof(ComponentContainerMonoLinker))]
    public class Entity : OptimizedMonoBehavior
    {
        public event Action<Type> ComponentRemoved;
        public event Action<Type> ComponentAdded;
        
        public ComponentContainerMonoLinker ComponentContainerMonoLinker
        {
            get
            {
                if (_componentContainerMonoLinker == null)
                {
                    _componentContainerMonoLinker = GetComponent<ComponentContainerMonoLinker>();
                    _componentContainerMonoLinker.Init();
                    OnComponentContainerInitialize();
                    _componentContainerMonoLinker.ComponentsContainer.ComponentAdded += OnComponentAdded;
                    _componentContainerMonoLinker.ComponentsContainer.ComponentRemoved += OnComponentRemoved;
                }

                return _componentContainerMonoLinker;
            }
        }

        private void OnDestroy()
        {
            _componentContainerMonoLinker.ComponentsContainer.ComponentAdded -= OnComponentAdded;
            _componentContainerMonoLinker.ComponentsContainer.ComponentRemoved -= OnComponentRemoved;
            _componentContainerMonoLinker.ComponentsContainer.Dispose();
        }

        private void OnComponentAdded(Type type)
        {
            ComponentAdded?.Invoke(type);
        }

        private void OnComponentRemoved(Type type)
        {
            ComponentRemoved?.Invoke(type);
        }

        private ComponentContainerMonoLinker _componentContainerMonoLinker;
        
        protected virtual void OnComponentContainerInitialize(){}
    }
}