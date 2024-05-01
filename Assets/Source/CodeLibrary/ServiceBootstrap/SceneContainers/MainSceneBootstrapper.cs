using Source.Scripts.PlayerModule;
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
