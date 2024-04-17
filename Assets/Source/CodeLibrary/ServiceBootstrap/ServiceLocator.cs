using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.CodeLibrary.ServiceBootstrap
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _global;
        private static Dictionary<Scene, ServiceLocator> _sceneContainers;
        
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly ServiceManager _services = new ServiceManager();
        private static List<GameObject> _tmpSceneGameObjects;

        internal void ConfigureAsGlobal(bool dontDestroyOnLoad)
        {
            if (_global == this)
            {
                Debug.LogWarning($"ServiceLocator.ConfigureAsGlobal: Already configured as global", this);
            } else if (_global != null)
            {
                Debug.LogWarning($"ServiceLocator.ConfigureAsGlobal: Another ServiceLocator is already configured as global", this);
                
                Destroy(gameObject);
            }
            else
            {
                _global = this;

                if (dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
        }

        internal void ConfigureForScene()
        {
            Scene scene = gameObject.scene;

            if (_sceneContainers.ContainsKey(scene))
            {
                Debug.LogWarning($"ServiceLocator.ConfigureForScene: Another ServiceLocator is already configured for this scene", this);
                return;
            }
            
            _sceneContainers.Add(scene, this);
        }
        
        public static ServiceLocator Global
        {
            get
            {
                if (_global != null) return _global;

                if (FindObjectOfType<ServiceLocatorGlobalBootstrapper>() is { } found)
                {
                    found.BootstrapOnDemand();

                    return _global;
                }

                GameObject container = new GameObject(BootstrapConstants.GlobalServiceLocatorName, typeof(ServiceLocator));

                container.AddComponent<ServiceLocatorGlobalBootstrapper>().BootstrapOnDemand();

                return _global;
            }
        }

        public static ServiceLocator For(MonoBehaviour behaviour)
        {
            return behaviour.GetComponentInParent<ServiceLocator>() == null ? ForSceneOf(behaviour) : Global;
        }

        public static ServiceLocator ForSceneOf(MonoBehaviour behaviour)
        {
            Scene scene = behaviour.gameObject.scene;

            if (_sceneContainers.TryGetValue(scene, out ServiceLocator containers) && containers != behaviour)
            {
                return containers;
            }
            
            _tmpSceneGameObjects.Clear();
            scene.GetRootGameObjects(_tmpSceneGameObjects);

            foreach (GameObject go in _tmpSceneGameObjects.Where(go => go.GetComponent<ServiceLocatorSceneBootstrapper>() != null))
            {
                if (go.TryGetComponent(out ServiceLocatorSceneBootstrapper bootstrapper) && bootstrapper.Container != behaviour)
                {
                    bootstrapper.BootstrapOnDemand();
                    return bootstrapper.Container;
                }
            }

            return Global;
        }
        
        public ServiceLocator Rebind(Type type, object service)
        {
            _services.Rebind(type, service);

            return this;
        }
        
        public ServiceLocator Register<T>(object service)
        {
            _services.Register(typeof(T), service);

            if(service is IDisposable disposable)
                _compositeDisposable.Add(disposable);

            return this;
        }
        
        public ServiceLocator Register(Type type, object service)
        {
            _services.Register(type, service);

            if(service is IDisposable disposable)
                _compositeDisposable.Add(disposable);
            
            return this;
        }

        public ServiceLocator Get<T>(out T service) where T : class
        {
            if (TryGetService(out service)) return this;

            if (TryGetNextInHierarchy(out ServiceLocator container))
            {
                container.Get(out service);
                return this;
            }

            throw new ArgumentException($"ServiceLocator.Get: Service of type {typeof(T).FullName} not registered");
        }
        
        public T Get<T>() where T : class
        {
            if(_services.Contains<T>())
                return _services.Get<T>();
            
            throw new ArgumentException($"ServiceLocator.Get: Service of type {typeof(T).FullName} not registered");
        }
        
        public bool TryGetService<T>(out T service) where T : class
        {
            return _services.TryGet(out service);
        }

        public bool TryGetNextInHierarchy(out ServiceLocator container)
        {
            if (this == _global)
            {
                container = null;
                return false;
            }

            if (transform.parent != null)
            {
                ServiceLocator nextLocator = transform.parent.GetComponentInParent<ServiceLocator>();

                if (nextLocator != null)
                {
                    container = nextLocator;
                }
                else
                {
                    container = ForSceneOf(this);
                }
            }
            else
            {
                container = null;
            }

            return container != null;
        }

        public void Clear() => _compositeDisposable.Dispose();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetStatics()
        {
            _global = null;
            _sceneContainers = new Dictionary<Scene, ServiceLocator>();
            _tmpSceneGameObjects = new List<GameObject>();
        }

        private void OnDestroy()
        {
            if (this == _global) _global = null;
            else if (_sceneContainers != null && _sceneContainers.ContainsValue(this))
            {
                _sceneContainers.Remove(gameObject.scene);
            }
        }
    }
}