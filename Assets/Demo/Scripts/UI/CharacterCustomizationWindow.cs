using Kha.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Kha.UI.Demo
{
    public class CharacterCustomizationWindow : UIEntity
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _showNotificationButton;

        private UISystem _uiSystem;

        protected override void OnAppearanceStarted()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClick);
            _showNotificationButton.onClick.AddListener(OnShowNotificationButtonClick);
        }

        protected override void OnDisappearanceFinished()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
            _showNotificationButton.onClick.RemoveListener(OnShowNotificationButtonClick);
        }

        public void Init(UISystem uiSystem)
        {
            _uiSystem = uiSystem;
        }

        private void OnCloseButtonClick()
        {
            _uiSystem.Windows.Hide<CharacterCustomizationWindow>();
        }
        
        private void OnShowNotificationButtonClick()
        {
            _uiSystem.Panels
                .Show<NotificationPanel, UIAnimationScaleUp, UIAnimationScaleDown>()
                .Init(_uiSystem);
        }
    }
}