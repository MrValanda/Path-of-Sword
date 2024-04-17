using UnityEngine;

namespace Source.CodeLibrary.ServiceBootstrap
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ServiceLocator))]
    public abstract class Bootstrapper : MonoBehaviour
    {
        private ServiceLocator _container;
        internal ServiceLocator Container => _container ??= _container = GetComponent<ServiceLocator>();

        private bool _hasBeenBootstrapped;

        private void Awake() => BootstrapOnDemand();

        public void BootstrapOnDemand()
        {
            if(_hasBeenBootstrapped) return;

            _hasBeenBootstrapped = true;

            Bootstrap();
        }

        protected abstract void Bootstrap();

        protected virtual void OnDestroy() => Container.Clear();
    }
}