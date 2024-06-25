using System;
using Source.Scripts;
using UnityEngine;

[Serializable]
public abstract class Transition
{
    [field: SerializeField] public State NextState { get; private set; }
    [field: SerializeField] public float Priority { get; private set; }
    public event Action<Transition> NeedTransit;
        
    public virtual void OnEnable()
    {
    }
    public virtual void OnDisable()
    {
    }

    protected void OnNeedTransit(Transition obj)
    {
        NeedTransit?.Invoke(obj);
    }
}