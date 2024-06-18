using System;
using Source.Scripts.EntityLogic;
using UniRx;


namespace Source.Modules.Tools
{
    public class OneFrameComponent<T> : IDisposable where T: class
    {
        private readonly IDisposable _disposable;
        
        public OneFrameComponent(Entity entity)
        {
            _disposable = Observable.EveryEndOfFrame().Take(1).Subscribe(_ =>
            {
                entity.Remove<T>();
            });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
