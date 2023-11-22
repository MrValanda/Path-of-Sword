using System;
using Tools;
using UnityEngine;

public class AttackEventListener : OptimizedMonoBehavior
{
    public event Action AttackEnded;

    public event Action AttackReset;

    public event Action AttackAnimationEnd;

    public event Action AttackStarted;

    [SerializeField] private AnimationHandler _animationHandler;

    private void StartAttack()
    {
        if (_animationHandler.IsInTransition)
        {
            return;
        }

        AttackStarted?.Invoke();
    }

    private void ResetAttack()
    {
        if (_animationHandler.IsInTransition)
        {
            return;
        }

        AttackReset?.Invoke();
    }

    private void EndAttack()
    {
        if (_animationHandler.IsInTransition)
        {
            return;
        }

        AttackEnded?.Invoke();
    }

    private void AnimationEnd()
    {
        if (_animationHandler.IsInTransition)
        {
            return;
        }

        AttackAnimationEnd?.Invoke();
    }
}