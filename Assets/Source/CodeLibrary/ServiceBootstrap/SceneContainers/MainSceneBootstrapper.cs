using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Source.Modules.CameraModule.Scripts;
using Source.Modules.CompositeRootModule;
using UnityEngine;

namespace Source.CodeLibrary.ServiceBootstrap.SceneContainers
{
    public class MainSceneBootstrapper : ServiceLocatorSceneBootstrapper
    {
        [SerializeField] private PlayerCompositeRoot _playerCompositeRoot;
        [SerializeField] private EnemyCompositeRoot _enemyCompositeRoot;
        [SerializeField] private List<CinemachineVirtualCamera> _cinemachineVirtualCameraBases;

        protected override void Bootstrap()
        {
            base.Bootstrap();
            List<CinemachineBasicMultiChannelPerlin> channelPerlins =
                _cinemachineVirtualCameraBases.Select(x => x.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>())
                    .ToList();

            Container.Register<CameraShakeService>(new CameraShakeService(channelPerlins));
            _playerCompositeRoot.Compose();
            _enemyCompositeRoot.Compose();
        }
    }
}
