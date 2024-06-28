using System;

namespace Source.Scripts.Interfaces
{
    public interface IComponentContainer
    {
        public event Action<Type> ComponentAdded;
        public event Action<Type> ComponentRemoved;
        public IComponentContainer AddComponent<T>(T component) where T : class;
        public T AddOrGetComponent<T>() where T : class, new();

        public bool TryGetComponent<T>(out T component) where T : class;

        public T GetComponent<T>() where T : class;

        public void RemoveComponent<T>(T component) where T : class;
    }
}