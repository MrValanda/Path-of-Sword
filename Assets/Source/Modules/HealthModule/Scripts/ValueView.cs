using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Modules.HealthModule.Scripts
{
    public class ValueView : MonoBehaviour
    {
        [SerializeField] private Image _currentValueImage;
        [SerializeField] private Image _changedValueImage;
        [SerializeField] private float _speed;
        [SerializeField] private Ease _ease;
        
        public void UpdateValue(float fillAmount)
        {
            if (_changedValueImage.fillAmount > fillAmount)
            {
                FillAmountSequence(_currentValueImage,_changedValueImage,fillAmount);
            }
            else
            {
                FillAmountSequence(_changedValueImage,_currentValueImage,fillAmount);
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