using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _oritentation;
    [SerializeField] private float _rotationSpeed;


    private static readonly int Speed = Animator.StringToHash("Speed");

    private Vector3 _moveDirection;

    private void Update()
    {
       
    }
}