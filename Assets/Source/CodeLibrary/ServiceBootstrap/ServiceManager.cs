using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.CodeLibrary.ServiceBootstrap
{
    public class ServiceManager
    {
        public IEnumerable<object> RegisteredServices => _services.Values;
        
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        
        public bool TryGet<T>(out T service) where T : class
        {
            Type type = typeof(T);

            if (_services.TryGetValue(type, out object @object))
            {
                service = @object as T;
                return true;
            }

            service = null;
            return false;
        }
        
        public bool Contains<T>() where T : class => _services.ContainsKey(typeof(T));

        public T Get<T>() where T : class
        {
            Type type = typeof(T);

            if (_services.TryGetValue(type, out object service))
                return service as T;

            throw new ArgumentException($"ServiceManager.Register: Service of type {type.FullName} not registered");
        }

        public ServiceManager Register<T>(T service)
        {
            Type type = typeof(T);

            if (!_services.TryAdd(type, service))
                Debug.Log($"ServiceManager.Register: Service of type {type.FullName} already registered");

            return this;
        }

        public ServiceManager Register(Type type, object service)
        {
            if (!type.IsInstanceOfType(service))
                throw new ArgumentException("Type of service does not match type of service interface",
                    nameof(service));
            
            if (!_services.TryAdd(type, service))
                Debug.Log($"ServiceManager.Register: Service of type {type.FullName} already registered");

            return this;
        }
        
        public ServiceManager Rebind(Type type, object service)
        {
            if (!type.IsInstanceOfType(service))
                throw new ArgumentException("Type of service does not match type of service interface", nameof(service));

            _services[type] = service;

            return this;
        }
    }
}
