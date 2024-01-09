using Kha.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Kha.UI.Demo
{
    public class SettingsWindow : UIEntity
    {
        [SerializeField] private Button _closeButton;

        private UISystem _uiSystem;

        protected override void OnAppearanceStarted()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        protected override void OnDisappearanceFinished()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        public void Init(UISystem uiSystem)
        {
            _uiSystem = uiSystem;
        }

        private void OnCloseButtonClick()
        {
            _uiSystem.Windows.Hide<SettingsWindow>();
        }
    }
}