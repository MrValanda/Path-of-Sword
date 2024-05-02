using System.Linq;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Source.Modules.JobsMethodsModule
{
    public static class JobsRaycast 
    {
        public static ColliderHit[] OverlapSphere(Vector3 point,float radius,QueryParameters queryParameters = default,int maxCollideCount = 20)
        {
            NativeArray<ColliderHit> result = new NativeArray<ColliderHit>(maxCollideCount, Allocator.TempJob);
            NativeArray<OverlapSphereCommand> commands = new NativeArray<OverlapSphereCommand>(1, Allocator.TempJob);
            commands[0] = new OverlapSphereCommand(point,radius,queryParameters);
            JobHandle jobHandle = OverlapSphereCommand.ScheduleBatch(commands, result, 1, maxCollideCount);
            jobHandle.Complete();
            ColliderHit[] colliderHits = result.Where(x=>x.collider != null).ToArray();
            result.Dispose();
            commands.Dispose();
            return colliderHits;
        }

        public static RaycastHit[] RayCast(Vector3 from,Vector3 direction,QueryParameters queryParameters = default,float distance = 3333,int maxCollideCount = 20)
        {
            NativeArray<RaycastHit> result = new NativeArray<RaycastHit>(maxCollideCount, Allocator.TempJob);
            NativeArray<RaycastCommand> commands = new NativeArray<RaycastCommand>(1, Allocator.TempJob);

            commands[0] = new RaycastCommand(from, direction, queryParameters, distance);
            JobHandle jobHandle = RaycastCommand.ScheduleBatch(commands, result, 1, maxCollideCount);
            jobHandle.Complete();
            RaycastHit[] raycastHits = result.Where(x=>x.collider != null).ToArray();
            result.Dispose();
            commands.Dispose();
            return raycastHits;

        }
    }
}
