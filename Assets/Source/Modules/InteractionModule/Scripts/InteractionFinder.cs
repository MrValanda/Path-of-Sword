using System;
using System.Collections.ObjectModel;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Source.Modules.InteractionModule.Scripts
{
    public class InteractionFinder : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable;
        public ObservableCollection<InteractionMono> FoundedInteractions { get; private set; } = new();

        public ObservableCollection<Transform> FoundedInteractionsTransforms { get; private set; } = new();
        
        public InteractionFinder(Collider detectCollider)
        {
            _compositeDisposable = new CompositeDisposable();
            _compositeDisposable.Add(detectCollider.OnTriggerEnterAsObservable().Subscribe(OnTriggerEnter));
            _compositeDisposable.Add(detectCollider.OnTriggerExitAsObservable().Subscribe(OnTriggerExit));
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractionMono interaction))
            {
                FoundedInteractions.Add(interaction);
                FoundedInteractionsTransforms.Add(interaction.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out InteractionMono interaction))
            {
                FoundedInteractions.Remove(interaction);
                FoundedInteractionsTransforms.Remove(interaction.transform);
            }
        }
    }
}
