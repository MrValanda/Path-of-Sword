using System;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;

public class DodgeEventListener :  OptimizedMonoBehavior
{
    public event Action DodgeEnded;

    private void DodgeEnd()
    {
        DodgeEnded?.Invoke();
    }
}