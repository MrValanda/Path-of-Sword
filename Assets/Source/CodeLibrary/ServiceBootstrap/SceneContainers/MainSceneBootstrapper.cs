using Source.Scripts_DONT_USE_THIS_FOLDER_.PlayerModule;
using UnityEngine;

namespace Source.CodeLibrary.ServiceBootstrap.SceneContainers
{
    public class MainSceneBootstrapper : ServiceLocatorSceneBootstrapper
    {
        [SerializeField] private PlayerCompositeRoot _playerCompositeRoot;
        protected override void Bootstrap()
        {
            base.Bootstrap();
            
            _playerCompositeRoot.Compose();
        }
    }
}
