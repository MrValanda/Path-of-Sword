using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemy
{
    [RequireComponent(typeof(CharacterController))]
    public class EnemyMovement : MonoBehaviour, IMovement
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
       
        [SerializeField] private Animator _animator;

        private CharacterController _characterController;
        private float _speed;
        private CancellationTokenSource _cancellationTokenSource;
        public bool CanMove { get; private set; } = true;
        
        public void Init(float speed)
        {
            _characterController = GetComponent<CharacterController>();
            _speed = speed;
            CanMove = true;
        }

        private void LateUpdate()
        {
            Vector3 velocity = _characterController.velocity;
            velocity.y = 0;

            _animator.SetFloat(Speed, velocity.magnitude / _speed, 0.1f, Time.deltaTime);
        }

        public void Move(Vector3 direction)
        {
            if (CanMove == false)
            {
                return;
            }

            _characterController.SimpleMove(direction * _speed);
        }

        public void PushByDirection(Vector3 force, float time)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            StartMoveToTargetAnimation(force, time, _cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid StartMoveToTargetAnimation(Vector3 force, float time,
            CancellationToken cancellationToken)
        {
            if (_characterController == null)
                return;

            float timer = 0;
            CanMove = false;

            if (_characterController != null)
            {
                _characterController.SimpleMove(force);
            }

            while (timer < time && cancellationToken.IsCancellationRequested == false)
            {
                timer += Time.deltaTime;
                _characterController.SimpleMove(force);
                await UniTask.Yield();
            }

            _characterController.SimpleMove(Vector3.zero);
            if (cancellationToken.IsCancellationRequested == false)
            {
                CanMove = true;
            }
        }
    }
}