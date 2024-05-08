using Source.Modules.CompositeRootModule;
using UnityEngine;

namespace Source.CodeLibrary.ServiceBootstrap.SceneContainers
{
    public class MainSceneBootstrapper : ServiceLocatorSceneBootstrapper
    {
        [SerializeField] private PlayerCompositeRoot _playerCompositeRoot;
        [SerializeField] private EnemyCompositeRoot _enemyCompositeRoot;
        protected override void Bootstrap()
        {
            base.Bootstrap();
            
            _playerCompositeRoot.Compose();
            _enemyCompositeRoot.Compose();
        }
    }
}
