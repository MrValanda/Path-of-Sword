using System;
using System.Linq;
using Source.Scripts.CannonSystem;
using Source.Scripts.InterfaceLinker;
using UniRx;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Source.Modules.DamageableFindersModule
{
    public class DamageableSelector : IDisposable
    {
        public event Action<DamageableLinker> SelectedChanged;
        
        private Transform _startingTransform;
        private DamageableFinder _damageableFinder;

        public DamageableLinker SelectedDamageable { get; private set; }
        private IDisposable _disposable;

        public void Initialize(Transform startingTransform,DamageableFinder damageableFinder)
        {
            _startingTransform = startingTransform;
            _damageableFinder = damageableFinder;
            
            _disposable = Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1f))
                .Subscribe(_ => SelectTarget());
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        private void SelectTarget()
        {
            if (_damageableFinder.FindedDamageables.Count == 0)
            {
                SelectedDamageable = null;
                return;
            }

            TransformAccessArray transformAccessArray = new(_damageableFinder.FindedDamageablesTransforms.ToArray());
            NativeArray<float> distances =
                new(_damageableFinder.FindedDamageablesTransforms.Count,
                    Allocator.TempJob);

            NativeArray<int> result = new(1, Allocator.TempJob);
            CalculateDistanceJob calculateDistanceJob = new()
            {
                Distances = distances,
                FromPosition = _startingTransform.position
            };
            FindNearestUnitJob findNearestUnit = new()
            {
                Distances = distances,
                ResultIndex = result,
                MinDistance = float.MaxValue
            };
            JobHandle schedule = calculateDistanceJob.Schedule(transformAccessArray);
            JobHandle jobHandle =
                findNearestUnit.Schedule(_damageableFinder.FindedDamageablesTransforms.Count, schedule);
            JobHandle.CompleteAll(ref schedule, ref jobHandle);

            SelectedDamageable = _damageableFinder.FindedDamageables[result[0]];
            distances.Dispose();
            result.Dispose();
            transformAccessArray.Dispose();
        }
    }
}