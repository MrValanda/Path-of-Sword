using System;
using Lean.Pool;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Factories
{
    public abstract class AbstractFactory<TFactoryType, TFactoryValue> : IDisposable where TFactoryValue : Component
    {
        private IFactory<TFactoryValue, TFactoryType> _factory;
        
        public TFactoryValue SpawnFactoryItem(TFactoryType type, Vector3 spawnPoint, bool useDefaultRotation = false)
        {
            TFactoryValue value = GetFactoryItem(type);
            
            return LeanPool.Spawn(value, spawnPoint,
                useDefaultRotation == false ? Quaternion.identity : value.transform.rotation);
        }

        public TFactoryValue SpawnFactoryItem(TFactoryType type, Transform parent,
            bool onlySpawnParentPosition = false)
        {
            TFactoryValue value = LeanPool.Spawn(GetFactoryItem(type), parent);
            
            if (onlySpawnParentPosition) 
                value.transform.parent = null;

            return value;
        }

        public TFactoryValue GetFactoryItem(TFactoryType type) => (_factory ??= GetSetup()).GetFactoryValue(type);

        protected abstract IFactory<TFactoryValue, TFactoryType> GetSetup();
        protected abstract void ReleaseSetup();

        public void Dispose() => ReleaseSetup();
    }
}
