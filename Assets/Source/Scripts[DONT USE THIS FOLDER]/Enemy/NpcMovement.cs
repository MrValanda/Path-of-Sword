using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Source.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

namespace Source.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NpcMovement : MonoBehaviour, IMovement
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _angle = 60f;
        [SerializeField] private bool _autoInit;

        [SerializeField, ShowIf(nameof(_autoInit))]
        private float _defaultSpeed = 8;

        [SerializeField, ShowIf(nameof(_autoInit))]
        private float _defaultAccelerations = 1111;

        private NavMeshAgent _navMeshAgent;
        private float _speed;
        private float _acceleration;
        public bool CanMove { get; private set; } = true;
        private Vector3 _normal;
        private float _minDotProduct;
        private CancellationTokenSource _cancellationTokenSource;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Start()
        {
            if (_autoInit)
            {
                Init(_defaultSpeed, _defaultAccelerations);
            }
        }

        public void Init(float speed, float acceleration)
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = speed;
            _navMeshAgent.acceleration = acceleration;
            _navMeshAgent.enabled = true;
            _speed = speed;
            _acceleration = acceleration;
            CanMove = true;
            _minDotProduct = Mathf.Cos(_angle * Mathf.Deg2Rad);
        }


        private void FixedUpdate()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            velocity.y = 0;
            _animator?.SetFloat(Speed, velocity.magnitude / _speed);

            _normal = Vector3.zero;
        }

        public void Move(Vector3 direction)
        {
            if (CanMove == false)
            {
                return;
            }

            if (direction == Vector3.zero)
            {
                _navMeshAgent.SetDestination(transform.position);
            }
            else
            {
                //   NavMesh.SamplePosition(direction, out NavMeshHit hit, 100f,NavMesh.AllAreas);
                _navMeshAgent.SetDestination(direction);
            }
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
            float timer = 0;
            CanMove = false;

            force = Vector3.ProjectOnPlane(force, _normal);
            _navMeshAgent.velocity = force;
            while (timer < time && cancellationToken.IsCancellationRequested == false)
            {
                timer += Time.deltaTime;
                _navMeshAgent.velocity = force;
                await UniTask.Yield();
            }

            _navMeshAgent.velocity = Vector3.zero;
            if (cancellationToken.IsCancellationRequested == false)
            {
                CanMove = true;
            }
        }

        private void OnCollisionStay(Collision other)
        {
            foreach (ContactPoint contact in other.contacts)
            {
                if (contact.normal.y >= _minDotProduct)
                {
                    _normal += contact.normal;
                }
            }
        }
    }
}