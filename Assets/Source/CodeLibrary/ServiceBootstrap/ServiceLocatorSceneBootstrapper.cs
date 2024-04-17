using UnityEngine;

namespace Source.CodeLibrary.ServiceBootstrap
{
    [DefaultExecutionOrder(-11000)]
    public class ServiceLocatorSceneBootstrapper : Bootstrapper
    {
        protected override void Bootstrap() => Container.ConfigureForScene();
    }
}