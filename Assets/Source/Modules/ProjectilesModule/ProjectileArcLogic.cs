using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using Source.Scripts.Setups;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.ProjectilesLogic
{
    public class ProjectileArcLogic : MonoBehaviour
    {
        [FormerlySerializedAs("_circleIndicatorHandler")] [SerializeField]
        private IndicatorHandler.IndicatorHandler indicatorHandler;

        [SerializeField] private MeshRenderer _meshRenderer;
        public event Action ProjectileReachedTarget;

        protected IndicatorDataSetup _indicatorDataSetup;
        protected LayerMask _obstacles;

        public void InitIndicator(IndicatorDataSetup indicatorDataSetup, LayerMask obstacles)
        {
            _indicatorDataSetup = indicatorDataSetup;
            _obstacles = obstacles;
        }

        [Button]
        public async void LaunchProjectile(Vector3 targetPosition, float jumpPower, float duration, Ease ease,
            float delay)
        {
            var circleIndicatorHandler = Instantiate(indicatorHandler, targetPosition, Quaternion.identity);
            targetPosition.y += transform.localScale.y / 2;
            circleIndicatorHandler.Init(_indicatorDataSetup, _obstacles);
            _meshRenderer.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            _meshRenderer.enabled = true;
            transform.parent = null;
            Sequence doJump = transform.DOJump(targetPosition, jumpPower, 1, duration).SetEase(ease);

            doJump.OnUpdate(() => { circleIndicatorHandler.SetDuration(doJump.ElapsedPercentage()); }).OnComplete(() =>
            {
                ProjectileReachedTarget?.Invoke();
                OnProjectileReachedTarget();
                Destroy(circleIndicatorHandler.gameObject);
            });
        }

        protected virtual void OnProjectileReachedTarget()
        {
        }
    }
}