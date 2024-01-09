using Kha.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Kha.UI.Demo
{
    public class BuildingTipWorldWindow : WorldWindow
    {
        [SerializeField] private Button _closeButton;

        private UISystem _uiSystem;
        private UIOwner _uiOwner;

        protected override void OnAppearanceStarted()
        {
            base.OnAppearanceStarted();
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        protected override void OnDisappearanceFinished()
        {
            base.OnDisappearanceFinished();
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        public void Init(UISystem uiSystem, UIOwner uiOwner)
        {
            _uiSystem = uiSystem;
            _uiOwner = uiOwner;
        }

        private void OnCloseButtonClick()
        {
            _uiSystem.WorldWindows.Hide<BuildingTipWorldWindow>(_uiOwner);
        }
    }
}

