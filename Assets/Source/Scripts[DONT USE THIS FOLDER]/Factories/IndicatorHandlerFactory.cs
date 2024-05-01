using System;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;

namespace Source.Scripts.Factories
{
    public class IndicatorHandlerFactory : AbstractFactory<Type, IndicatorHandler.IndicatorHandler>
    {
        protected override IFactory<IndicatorHandler.IndicatorHandler, Type> GetSetup() => IndicatorHandlerFactorySetup.GetInstance();

        protected override void ReleaseSetup() => IndicatorHandlerFactorySetup.ClearAllReferences();
    }
}