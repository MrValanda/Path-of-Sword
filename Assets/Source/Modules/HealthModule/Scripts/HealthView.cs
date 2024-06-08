using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Modules.HealthModule.Scripts
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _currentHealthImage;
        [SerializeField] private Image _damageHealthImage;
        [SerializeField] private float _speed;
        [SerializeField] private Ease _ease;

        [Button]
        public void UpdateHealth(float fillAmount)
        {
            if (_damageHealthImage.fillAmount > fillAmount)
            {
                FillAmountSequence(_currentHealthImage,_damageHealthImage,fillAmount);
            }
            else
            {
                FillAmountSequence(_damageHealthImage,_currentHealthImage,fillAmount);
            }
        }

        private void FillAmountSequence(Image firstFillImage,Image secondFillImage,float fillAmount)
        {
            firstFillImage.DOKill();
            secondFillImage.DOKill();
            firstFillImage.DOFillAmount(fillAmount, _speed).SetSpeedBased(true).SetEase(_ease).OnComplete(() =>
            {
                secondFillImage.DOFillAmount(fillAmount, _speed).SetSpeedBased(true).SetEase(_ease);
            });
        }
    }
}
