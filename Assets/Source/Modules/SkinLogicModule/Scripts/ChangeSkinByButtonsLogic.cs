using System.Collections.Generic;
using SkinLogic;
using Source.CodeLibrary.ServiceBootstrap;
using Source.Modules.SkinLogicModule.Scripts;
using Source.Scripts.PlayerLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.SkinLogic
{
    public class ChangeSkinByButtonsLogic : MonoBehaviour
    {
        [SerializeField] private Button _nextSkinPartButton;
        [SerializeField] private Button _previousSkinPartButton;
        [SerializeField] private Button _clear;
        [SerializeField] private List<SkinPart> _skinParts;

        private SkinView _skinView;
        private int _currentIndex;

        public void Start()
        {
            _currentIndex = _skinParts.FindIndex(x => _skinView.ContainsSkinPart(x));
            _nextSkinPartButton.onClick.AddListener(OnNextSkinPartButtonClick);
            _previousSkinPartButton.onClick.AddListener(OnPreviousSkinPartButtonClick);
            _clear.onClick.AddListener(OnClearButtonClick);
        }

        private void OnClearButtonClick()
        {
            if (_currentIndex == -1) return;
            
            _skinView.ClearSkinPart(_skinParts[_currentIndex].SkinPartType);
            _currentIndex = 0;
        }

        private void OnDestroy()
        {
            _nextSkinPartButton.onClick.RemoveListener(OnNextSkinPartButtonClick);
            _previousSkinPartButton.onClick.RemoveListener(OnPreviousSkinPartButtonClick);
        }

        private void OnPreviousSkinPartButtonClick()
        {
            UpdateIndex(-1);
            _skinView.UnitSkinParts.UpdateSkinPart(_skinParts[_currentIndex]);
        }

        private void OnNextSkinPartButtonClick()
        {
            UpdateIndex(1);
            _skinView.UnitSkinParts.UpdateSkinPart(_skinParts[_currentIndex]);
        }

        private void UpdateIndex(int counter)
        {
            _currentIndex += counter;
            if (_currentIndex < 0)
                _currentIndex = _skinParts.Count - 1;
            else if (_currentIndex > _skinParts.Count - 1) _currentIndex = 0;
        }
    }
}