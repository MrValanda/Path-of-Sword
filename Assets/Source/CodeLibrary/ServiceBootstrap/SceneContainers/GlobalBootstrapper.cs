using Source.Scripts.Factories;

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