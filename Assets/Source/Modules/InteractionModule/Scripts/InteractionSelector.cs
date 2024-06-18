using System;
using System.Linq;
using Source.Scripts.CannonSystem;
using UniRx;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Source.Modules.InteractionModule.Scripts
{
    public class InteractionSelector : IDisposable
    {
        public event Action<InteractionMono> SelectedChanged;
        
        private readonly Transform _startingTransform;
        private readonly InteractionFinder _interactionFinder;

        public InteractionMono SelectedInteraction { get; private set; }
        private readonly IDisposable _disposable;

        public InteractionSelector(Transform startingTransform,InteractionFinder interactionFinder)
        {
            _startingTransform = startingTransform;
            _interactionFinder = interactionFinder;
            
            _disposable = Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1f))
                .Subscribe(_ => SelectTarget());
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        private void SelectTarget()
        {
            
            if (_interactionFinder.FoundedInteractions.Count == 0)
            {
                SelectedInteraction = null;
                return;
            }

            TransformAccessArray transformAccessArray = new(_interactionFinder.FoundedInteractionsTransforms.ToArray());
            NativeArray<float> distances =
                new(_interactionFinder.FoundedInteractionsTransforms.Count,
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
                findNearestUnit.Schedule(_interactionFinder.FoundedInteractionsTransforms.Count, schedule);
            JobHandle.CompleteAll(ref schedule, ref jobHandle);

            SelectedInteraction = _interactionFinder.FoundedInteractions[result[0]];
            distances.Dispose();
            result.Dispose();
            transformAccessArray.Dispose();
            
        }
    }
}