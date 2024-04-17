using System;
using Source.CodeLibrary.ServiceBootstrap;
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
        [SerializeField] private Vector3 _vfxSize;
        [SerializeField] private TeleportCentre _teleportCentreType;


        public void ExecuteAction(Transform castPoint, Enemy.Enemy abilityCaster, AbilityDataSetup baseAbilitySetup)
        {
            InCirclePointFinderByRaycast inCirclePointFinderByRaycast = new InCirclePointFinderByRaycast();

            Vector3 teleportFindCentre = abilityCaster.PatrolPoints[0].PatrolPoint.position;
            switch (_teleportCentreType)
            {
                case TeleportCentre.Target:
                    teleportFindCentre = abilityCaster.Target.transform.position;
                    break;
                case TeleportCentre.PatrolPoint:
                    teleportFindCentre = abilityCaster.PatrolPoints[0].PatrolPoint.position;
                    break;
            }

            abilityCaster.transform.position =
                inCirclePointFinderByRaycast.FindFreePointInCircle(teleportFindCentre,
                    _maxTeleportDistance, _minTeleportDistance, 50);
        }
    }
}