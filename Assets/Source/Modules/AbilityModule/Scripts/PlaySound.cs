using System;
using Source.CodeLibrary.ServiceBootstrap;
using Source.Modules.AudioModule;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Modules.AbilityModule
{
    [Serializable]
    public class PlaySound : IAbilityAction
    {
        [SerializeField] private SoundType _soundType;
        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            ServiceLocator.For(abilityCaster).Get<SoundPlayer>().PlaySoundByType(_soundType);
        }
    }
}