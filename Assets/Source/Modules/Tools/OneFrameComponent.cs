using System;
using Source.Scripts.EntityLogic;
using UniRx;


namespace Source.Modules.Tools
{
    public class OneFrameComponent<T> : IDisposable where T: class
    {
        private Entity _entity;
        private readonly IDisposable _disposable;
        
        public OneFrameComponent(Entity entity)
        {
            _entity = entity;
            _disposable = Observable.EveryEndOfFrame().Take(1).Subscribe(_ =>
            {
                _entity.Remove<T>();
            });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
