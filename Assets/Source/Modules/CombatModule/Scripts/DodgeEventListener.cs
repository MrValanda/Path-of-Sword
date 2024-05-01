using System;

public class DodgeEventListener :  OptimizedMonoBehavior
{
    public event Action DodgeEnded;

    private void DodgeEnd()
    {
        DodgeEnded?.Invoke();
    }
}