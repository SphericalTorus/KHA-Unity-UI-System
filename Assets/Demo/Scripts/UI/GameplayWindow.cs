using Kha.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Kha.UI.Demo
{
    public class GameplayWindow : UIEntity
    {
        [SerializeField] private Button _showNotificationButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _specialOfferButton;
        [SerializeField] private Button _characterCustomizationButton;

        private UISystem _uiSystem;

        protected override void OnAppearanceStarted()
        {
            _showNotificationButton.onClick.AddListener(OnShowNotificationButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
            _specialOfferButton.onClick.AddListener(OnSpecialOfferButtonClick);
            _characterCustomizationButton.onClick.AddListener(OnCharacterCustomizationButtonClick);
        }

        protected override void OnDisappearanceFinished()
        {
            _showNotificationButton.onClick.RemoveListener(OnShowNotificationButtonClick);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
            _specialOfferButton.onClick.RemoveListener(OnSpecialOfferButtonClick);
            _characterCustomizationButton.onClick.RemoveListener(OnCharacterCustomizationButtonClick);
        }

        public void Init(UISystem uiSystem)
        {
            _uiSystem = uiSystem;
        }

        private void OnShowNotificationButtonClick()
        {
            _uiSystem.Panels.Show<NotificationPanel>(FlatShowStrategy.Default).Init(_uiSystem);
        }
        
        private void OnSettingsButtonClick()
        {
            _uiSystem.Windows.Show<SettingsWindow>(ShowInHierarchyStrategy.OnTop)
                .OnCreated(window => window.Init(_uiSystem));
        }
        
        private void OnSpecialOfferButtonClick()
        {
            _uiSystem.Windows.Show<SpecialOfferWindow>(ShowInHierarchyStrategy.OnTop)
                .OnCreated(window => window.Init(_uiSystem));
        }
        
        private void OnCharacterCustomizationButtonClick()
        {
            _uiSystem.Windows.Show<CharacterCustomizationWindow>(ShowInHierarchyStrategy.OnTop)
                .OnCreated(window => window.Init(_uiSystem));   
        }
    }
}