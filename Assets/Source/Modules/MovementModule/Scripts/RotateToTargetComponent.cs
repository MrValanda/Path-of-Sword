using System;
using Source.Scripts.EntityLogic;
using UniRx;
using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class RotateToTargetComponent : IDisposable
    {
        private IDisposable _disposable;

        public void Initialize(Entity sender, Transform target, float speed)
        {
            Dispose();
            _disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                Vector3 desiredForward = target.position - sender.transform.position;
                Vector3 currentForward = Vector3.Lerp(sender.transform.forward, desiredForward, speed * Time.deltaTime);
                currentForward.y = 0;
                sender.transform.forward = currentForward;
            });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}