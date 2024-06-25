using UnityEngine;

namespace Source.CodeLibrary.ServiceBootstrap
{ 
    [DefaultExecutionOrder(-12000)]
    public class ServiceLocatorGlobalBootstrapper : Bootstrapper
    {
        [SerializeField] private bool dontDestroyOnLoad = true;
        
        protected override void Bootstrap()
        {
            Container.ConfigureAsGlobal(dontDestroyOnLoad);
        }
    }
}