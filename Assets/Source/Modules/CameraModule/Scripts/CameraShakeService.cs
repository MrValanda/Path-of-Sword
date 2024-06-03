using System;
using System.Collections.Generic;
using Cinemachine;
using UniRx;
using UnityEngine;

namespace Source.Modules.CameraModule.Scripts
{
    public class CameraShakeService
    {
        private List<CinemachineBasicMultiChannelPerlin> _cameraShakeModules;
        private IDisposable _shakeDisposable;
        private float _currentIntensity;
        private float _currentTime;
        public CameraShakeService(List<CinemachineBasicMultiChannelPerlin> cameraShakeModules)
        {
            _cameraShakeModules = cameraShakeModules;
        }

        public void Shake(float intensity, float time)
        {
            _shakeDisposable?.Dispose();
            SetIntensity(intensity);
            _currentIntensity = intensity;
            _currentTime = 0;
            _shakeDisposable = Observable.EveryUpdate().TakeWhile(_ => _currentIntensity > 0).Subscribe(_ =>
            {
                _currentTime += Time.deltaTime;
                _currentIntensity = Mathf.Lerp(intensity, 0, _currentTime / time);
                SetIntensity(_currentIntensity);
            });
        }

        private void SetIntensity(float intensity)
        {
            foreach (var channelPerlin in _cameraShakeModules)
            {
                channelPerlin.m_AmplitudeGain = intensity;
            }
        }
    }
}
