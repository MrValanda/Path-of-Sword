using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Source.Modules.CameraModule.Scripts;
using Source.Scripts.Factories;
using UnityEngine;

namespace Source.CodeLibrary.ServiceBootstrap.SceneContainers
{
    public class GlobalBootstrapper : ServiceLocatorGlobalBootstrapper
    {
        private IndicatorHandlerFactory _indicatorHandlerFactory;

        protected override void Bootstrap()
        {
            base.Bootstrap();
            _indicatorHandlerFactory = new IndicatorHandlerFactory();
          
            Container.Register<IndicatorHandlerFactory>(_indicatorHandlerFactory);
        }
    }
}