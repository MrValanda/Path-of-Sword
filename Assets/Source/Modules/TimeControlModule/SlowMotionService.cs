using System;
using UniRx;
using UnityEngine;

namespace Source.Modules.TimeControlModule
{
    public class SlowMotionService
    {
        private IDisposable _disposable;

        public void ActiveSlowMotion(float slowMotionPower = 0.1f, float duration = 0.1f)
        {
            _disposable?.Dispose();
            Time.timeScale = slowMotionPower;
            Time.fixedDeltaTime = slowMotionPower * 0.02f;
            _disposable = Observable.Timer(TimeSpan.FromSeconds(duration), Scheduler.MainThreadIgnoreTimeScale)
                .Subscribe(_ =>
                {
                    Time.timeScale = 1;
                    Time.fixedDeltaTime = 1 * 0.02f;
                });
        }
    }
}