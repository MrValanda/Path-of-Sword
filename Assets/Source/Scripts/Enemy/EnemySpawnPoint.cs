using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.CodeLibrary.ServiceBootstrap;
using Source.CodeLibrary.Tools;
using Source.Scripts.InterfaceLinker;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups.Characters;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Source.Scripts.Enemy
{
    [Serializable]
    public struct EnemySpawnData
    {
        public Enemy EnemyBehavior;
        [InlineEditor] public EnemyCharacterSetup EnemyBalance;
        public int Lvl;
    }

    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private DamageableLinker _damageableLinker;

        [FormerlySerializedAs("id")] [SerializeField]
        public string _enemyKey;

        [SerializeField] public long _secondRespawn = 10;
        [SerializeField] public long _secondToHeal = 20;
        [SerializeField] private List<EnemySpawnData> _enemySpawnDatas = new List<EnemySpawnData>();
        [SerializeField] private List<PatrolPointData> _patrolPointDatas = new List<PatrolPointData>();
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        [SerializeField] private bool _autoInit;

        private IDisposable _disposable;
        private Enemy _spawnedEnemy;
        private EnemySubModel _enemySubModel;
        private CancellationTokenSource _cancellationTokenSource;

        private float _lastEnemyTakeDamage;

        private void Start()
        {
            _enemySubModel = new EnemySubModel(_enemyKey, 0);
            if (_autoInit)
            {
                _disposable = Observable
                    .Interval(TimeSpan.FromSeconds(0.1))
                    .Subscribe(
                        x =>
                        {
                            _disposable?.Dispose();
                            Init();
                        });
            }
        }

        public void Init()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            InitState();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private void InitState()
        {
            CheckActivation();
        }

        public void CheckActivation()
        {
            long currentTimeInTicks = DateTime.UtcNow.Ticks;

            if (currentTimeInTicks >= _enemySubModel.t)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            EnemySpawnData enemySpawnData = _enemySpawnDatas.Random();
            Vector3 spawnPoint = _spawnPoints.Count == 0 ? transform.position : _spawnPoints.Random().position;
            _spawnedEnemy = LeanPool.Spawn(enemySpawnData.EnemyBehavior, spawnPoint, Quaternion.identity,
                transform);
            _spawnedEnemy.transform.forward = transform.forward;
            if (_patrolPointDatas.Count == 0)
            {
                _patrolPointDatas.Add(new PatrolPointData() {PatrolPoint = transform, TimeToGetNextPoint = 10f});
            }

            _spawnedEnemy.Init(enemySpawnData.EnemyBalance, _damageableLinker, transform, _patrolPointDatas,
                enemySpawnData.Lvl, _enemyKey);
            _spawnedEnemy.ComponentContainer.GetComponent<IDying>().Dead += OnSpawnedEnemyDeath;
            _spawnedEnemy.ComponentContainer.GetComponent<IDamageable>().ReceivedDamage += OnSpawnedEnemyReceivedDamage;

            _lastEnemyTakeDamage = Time.time;
            StartCheckHeal(_cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid StartCheckHeal(CancellationToken cancellationToken)
        {
            while (_spawnedEnemy != null)
            {
                if (Time.time - _lastEnemyTakeDamage >= _secondToHeal)
                {
                    _spawnedEnemy.ReceiveHeal(_spawnedEnemy.DefaultHealth());
                    _lastEnemyTakeDamage = Time.time;
                }

                await UniTask.NextFrame(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        private void OnSpawnedEnemyReceivedDamage(double obj)
        {
            _lastEnemyTakeDamage = Time.time;
        }

        private void OnSpawnedEnemyDeath(IDying obj)
        {
            _spawnedEnemy.ComponentContainer.GetComponent<IDying>().Dead -= OnSpawnedEnemyDeath;
            _spawnedEnemy.ComponentContainer.GetComponent<IDamageable>().ReceivedDamage -= OnSpawnedEnemyReceivedDamage;
            _spawnedEnemy = null;
            DOVirtual.DelayedCall(_secondRespawn, SpawnEnemy);
            ResetSpawnTime();
        }

        private long GetRespawnTime()
        {
            long startRespawn = DateTime.UtcNow.Ticks + TimeSpan.TicksPerSecond * _secondRespawn;
            return startRespawn;
        }

        private void ResetSpawnTime()
        {
            _enemySubModel.SetState(GetRespawnTime());
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            EnemySpawnData enemySpawnData = _enemySpawnDatas.FirstOrDefault();
            if (enemySpawnData.EnemyBehavior == null) return;
            Gizmos.color = Color.red;
            SkinnedMeshRenderer[] skinnedMeshRenderers =
                enemySpawnData.EnemyBehavior.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var skinnedMesh in skinnedMeshRenderers)
            {
                Gizmos.DrawMesh(skinnedMesh.sharedMesh, transform.position, transform.rotation,
                    Vector3.one * 3);
            }
        }

        private void OnDrawGizmosSelected()
        {
            EnemySpawnData enemySpawnData = _enemySpawnDatas.FirstOrDefault();
            if (enemySpawnData.EnemyBalance == null) return;

            Handles.color = Color.yellow;
            Handles.DrawWireArc(transform.position, Vector3.up, transform.forward,
                enemySpawnData.EnemyBalance.ViewAngle, enemySpawnData.EnemyBalance.AttackRadius);

            Handles.color = Color.green;
            Handles.DrawWireArc(transform.position, Vector3.up, transform.forward,
                enemySpawnData.EnemyBalance.ViewAngle, enemySpawnData.EnemyBalance.DetectRadius);

            Handles.color = Color.cyan;
            Handles.DrawWireArc(transform.position, Vector3.up, transform.forward,
                enemySpawnData.EnemyBalance.ViewAngle, enemySpawnData.EnemyBalance.MaxChaseDistance);


            Handles.color = Color.white;
            List<Vector3> points = _patrolPointDatas.Select(x => x.PatrolPoint?.position ?? Vector3.zero).ToList();
            points.Insert(0, transform.position);
            Handles.DrawPolyLine(points.ToArray());
        }
#endif
    }
}