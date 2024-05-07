using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class AddForceDirectionComponent : IDisposable
    {
        public Entity WhoWillMoveEntity;
        private CancellationTokenSource _cancellationTokenSource;
 
        public void Execute(Vector3 force, float deceleration)
        {
            Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            AddForce(force, deceleration, _cancellationTokenSource.Token).Forget();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private async UniTaskVoid AddForce(Vector3 force, float deceleration,
            CancellationToken token)
        {
            Vector3 currentForce = force;
            while (token.IsCancellationRequested == false && currentForce != Vector3.zero)
            {
                WhoWillMoveEntity.Get<CharacterController>().SimpleMove(currentForce);
                currentForce = Vector3.Lerp(currentForce, Vector3.zero, Time.deltaTime * deceleration);
                await UniTask.Yield(token);
            }

            if (token.IsCancellationRequested == false)
                WhoWillMoveEntity.Remove<AddForceDirectionComponent>();
        }
    }
}