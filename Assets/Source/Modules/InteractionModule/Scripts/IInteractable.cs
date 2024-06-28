namespace Source.Scripts.Interfaces
{
    public interface IInteractable
    {
        public bool IsInteractionAvailable { get; }


        public void StopInteract() { }
    }
}