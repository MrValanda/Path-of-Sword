using System;
using System.Collections.Generic;
using Source.Scripts.Interfaces;
using UnityEngine.Rendering;

namespace Source.Scripts.Utils
{
    public class ComponentContainer : IComponentContainer
    {
        private readonly Dictionary<Type, object> _components = new();

        public IComponentContainer AddComponent<T>(T component) where T : class
        {
            _components[typeof(T)] = component;
            
            return this;
        }
        public void AddComponent<T>(T component,Type type) where T : class
        {
            _components[type] = component;
        }

        public T AddOrGetComponent<T>() where T : class, new()
        {
            Type component = typeof(T);
            
            if (_components.TryGetValue(component, out object component1))
                return component1 as T;

            T newComponent = new T();
            
            AddComponent(newComponent);
            
            return newComponent;
        }
        
        public bool TryGetComponent<T>(out T component) where T : class
        {
            bool success = _components.TryGetValue(typeof(T), out object desiredComponent);
            
            component = desiredComponent as T;

            return success && desiredComponent is T;
        }

        public T GetComponent<T>() where T : class => _components[typeof(T)] as T;

        public void RemoveComponent<T>(T component = null) where T : class
        {
            if(_components.ContainsKey(typeof(T)) == false) 
                return;
            
            _components.Remove(typeof(T));
        }
    }
}