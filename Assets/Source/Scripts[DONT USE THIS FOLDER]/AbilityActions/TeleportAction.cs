using System;
using Source.CodeLibrary.ServiceBootstrap;
using Source.Modules.DamageableFindersModule;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.ResourceFolder;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.AbilityActions
{
    [Serializable]
    public class TeleportAction : IAbilityAction
    {
        private enum TeleportCentre
        {
            Target = 0,
            PatrolPoint = 1,
        }

        [SerializeField] private float _minTeleportDistance;
        [SerializeField] private float _maxTeleportDistance;
        [SerializeField] private TeleportCentre _teleportCentreType;


        public void ExecuteAction(Transform castPoint, Entity abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            InCirclePointFinderByRaycast inCirclePointFinderByRaycast = new InCirclePointFinderByRaycast();

            Vector3 teleportFindCentre = Vector3.zero;
            switch (_teleportCentreType)
            {
                case TeleportCentre.Target:
                    teleportFindCentre = abilityCaster.Get<DamageableSelector>().SelectedDamageable.transform.position;
                    break;
            }

            abilityCaster.transform.position =
                inCirclePointFinderByRaycast.FindFreePointInCircle(teleportFindCentre,
                    _maxTeleportDistance, _minTeleportDistance, 50);
        }
    }
}