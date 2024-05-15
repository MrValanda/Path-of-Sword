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
            private Vector3 _previousExecuteHit;

            public RaycastHit[] ExecuteHit(int maxHitCount = 20)
            {
                if (_previousExecuteHit == Vector3.zero)
                {
                    _previousExecuteHit = hitBoxPoint.position;
                }
                QueryParameters queryParameters = new QueryParameters()
                    {hitBackfaces = true, hitMultipleFaces = true, hitTriggers = QueryTriggerInteraction.Collide,layerMask = LayerMask.GetMask()};
                RaycastHit[] raycastHits = JobsRaycast.RayCast(hitBoxPoint.position,
                    (_previousExecuteHit - hitBoxPoint.position).normalized,
                    queryParameters,11f, maxHitCount);
                Debug.DrawLine(_previousExecuteHit,hitBoxPoint.position);
                _previousExecuteHit = hitBoxPoint.position;
                
                return raycastHits;
            }
        }
    }
}