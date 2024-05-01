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
    }
}
