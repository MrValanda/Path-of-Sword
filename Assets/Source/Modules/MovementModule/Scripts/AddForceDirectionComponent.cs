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
        private Vector3 _previousForce;
 
        public void Execute(Vector3 force, float deceleration)
        {
            Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            _previousForce += force;
            AddForce(_previousForce, deceleration, _cancellationTokenSource.Token).Forget();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private async UniTaskVoid AddForce(Vector3 force, float deceleration,
            CancellationToken token)
        {
            _previousForce = force;
            while (token.IsCancellationRequested == false && _previousForce != Vector3.zero)
            {
                WhoWillMoveEntity.Get<CharacterController>().Move(_previousForce * Time.deltaTime);
                _previousForce = Vector3.Lerp(_previousForce, Vector3.zero, Time.deltaTime * deceleration);
                await UniTask.Yield(token);
            }

            if (token.IsCancellationRequested == false)
                WhoWillMoveEntity.Remove<AddForceDirectionComponent>();
        }
    }
}