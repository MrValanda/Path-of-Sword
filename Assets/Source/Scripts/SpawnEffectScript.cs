using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnEffectScript : MonoBehaviour
{
    [FormerlySerializedAs("_startAttackEventListener")] [SerializeField] private AttackEventListener attackEventListener;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<MoveSet> _moveSets;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _orientation;

    private int _currentAttackIndex;
    private int _currentAttackAnimationIndex;
    private bool _isAttacking;
    private AnimatorOverrideController _animatorOverrideController;
    private List<string> _attacksAnimators = new List<string>() {"Attack", "Attack2"};
    
    
    private void Update()
    {
        if (_isAttacking == false && Input.GetMouseButtonDown(0))
        {
            _playerMovement.enabled = false;
            Vector3 oritentationForward = _orientation.forward;
            Vector3 oritentationRight = _orientation.right;
            oritentationForward.y = 0;
            oritentationRight.y = 0;
            var _moveDirection = oritentationForward * Input.GetAxisRaw("Vertical") +
                                 oritentationRight * Input.GetAxisRaw("Horizontal");

            if (_moveDirection != Vector3.zero)
            {
                transform.forward = _moveDirection;
            }
            else
            {
                transform.forward = oritentationForward;
            }

            _animatorOverrideController[_attacksAnimators[_currentAttackAnimationIndex]] =
                _moveSets[_currentAttackIndex].AnimationClip;
            _animator.runtimeAnimatorController = _animatorOverrideController;
            _animator.CrossFade(_attacksAnimators[_currentAttackAnimationIndex++], 0.1f, 0, 0);
            _currentAttackAnimationIndex %= _attacksAnimators.Count;
            _isAttacking = true;
        }
    }

   
}

[Serializable]
public class MoveSet
{
    [field: SerializeField] public AnimationClip AnimationClip { get; private set; }
}