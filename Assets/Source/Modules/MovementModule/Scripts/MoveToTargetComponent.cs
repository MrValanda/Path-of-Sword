using System;
using Source.Scripts.EntityLogic;
using UniRx;
using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class MoveToTargetComponent : IDisposable
    {
        private IDisposable _disposable;

        public void Initialize(Entity sender, Transform target, float speed)
        {
            Dispose();
            _disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                Vector3 desiredPosition = target.position;
                Vector3 position = sender.transform.position;
                Vector3 currentPosition = Vector3.Lerp(position, desiredPosition, speed * Time.deltaTime);
                sender.Get<CharacterController>().Move(currentPosition - position);
            });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}