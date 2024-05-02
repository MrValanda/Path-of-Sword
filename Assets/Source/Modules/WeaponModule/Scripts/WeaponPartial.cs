using System;
using Source.Modules.JobsMethodsModule;
using UnityEngine;

namespace Source.Modules.WeaponModule.Scripts
{
    public partial class Weapon
    {
        private class EntityAttackData
        {
            public float LastAttackTime;
            public int AttackHits;
        }

        [Serializable]
        private class WeaponHitBox
        {
            public float radius;
            public Transform hitBoxPoint;
            public Transform lossyScaleFactor;

            public ColliderHit[] ExecuteHit(int maxHitCount = 20)
            {
                QueryParameters queryParameters = new QueryParameters()
                    {hitBackfaces = true, hitMultipleFaces = true, hitTriggers = QueryTriggerInteraction.Ignore,layerMask = LayerMask.GetMask()};
                return JobsRaycast.OverlapSphere(hitBoxPoint.position, radius * lossyScaleFactor.lossyScale.x,
                    queryParameters, maxHitCount);
            }
        }
    }
}