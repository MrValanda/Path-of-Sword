using System;
using System.Collections.Generic;
using Source.Scripts.Interfaces;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.Utils
{
    public class ComponentContainer : IComponentContainer
    {
        public event Action<Type> ComponentAdded;
        public event Action<Type> ComponentRemoved;
        
        private readonly Dictionary<Type, object> _components = new();
        
        public IComponentContainer AddComponent<T>(T component) where T : class
        {
            Type type = typeof(T);
            _components[type] = component;
            ComponentAdded?.Invoke(type);
            return this;
        }
        public void AddComponent<T>(T component,Type type) where T : class
        {
            _components[type] = component;
            ComponentAdded?.Invoke(type);
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

        public T GetComponent<T>() where T : class
        {
            if(_components.ContainsKey(typeof(T)) == false) 
                return null;
            return _components[typeof(T)] as T;
        }

        public void RemoveComponent<T>(T component = null) where T : class
        {
            Type type = typeof(T);
            if(_components.ContainsKey(type) == false) 
                return;
            if (_components[type] is IDisposable disposable)
            {
                disposable.Dispose();
            }
            ComponentRemoved?.Invoke(type);
            _components.Remove(type);
        }

        public bool ContainsComponent<T>() where T : class
        {
            return _components.ContainsKey(typeof(T));
        }
    }
}