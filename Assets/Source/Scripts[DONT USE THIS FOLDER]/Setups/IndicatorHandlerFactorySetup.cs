using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using Source.Scripts.Interfaces;
using Source.Scripts.Utils.SO;
using UnityEngine;

namespace Source.Scripts.Setups
{
    [CreateAssetMenu(fileName = "IndicatorHandlerFactorySetup", menuName = "Setups/Factory/IndicatorHandlerFactorySetup")]
    public class IndicatorHandlerFactorySetup : LoadableScriptableObject<IndicatorHandlerFactorySetup>, IFactory<IndicatorHandler.IndicatorHandler, Type>
    {
        [OdinSerialize] private Dictionary<Type, IndicatorHandler.IndicatorHandler> _indicatorHandlers;

        public IndicatorHandler.IndicatorHandler GetFactoryValue(Type factoryType)
        {
            if (!_indicatorHandlers.ContainsKey(factoryType))
            {
                Debug.LogError("Not contain {"+factoryType+"} GetFactoryValue in ResourcePart ");
                return null;
            }
            return _indicatorHandlers[factoryType];
        }
    }
}