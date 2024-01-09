using Kha.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Kha.UI.Demo
{
    public class SpecialOfferDetailsWindow : UIEntity
    {
        [SerializeField] private Button _detailsButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Text _detailLabel;

        private UISystem _uiSystem;

        protected override void OnAppearanceStarted()
        {
            _detailsButton.onClick.AddListener(OnDetailsButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
            _backButton.onClick.AddListener(OnBackButtonClick);
        }

        protected override void OnDisappearanceFinished()
        {
            _detailsButton.onClick.RemoveListener(OnDetailsButtonClick);
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
            _backButton.onClick.RemoveListener(OnBackButtonClick);
        }

        public void Init(UISystem uiSystem)
        {
            _uiSystem = uiSystem;
            _detailLabel.text = $"SOME BORING FACT ABOUT SPECIAL OFFER #{UnityEngine.Random.Range(0, 9999)}";
        }

        private void OnDetailsButtonClick()
        {
            _uiSystem.Windows.Show<SpecialOfferDetailsWindow>(ShowInHierarchyStrategy.HideAllCut)
                .OnCreated(window => window.Init(_uiSystem));
        }
        
        private void OnCloseButtonClick()
        {
            _uiSystem.Windows.Show<GameplayWindow>(ShowInHierarchyStrategy.ReleaseAllCut)
                .OnCreated(window => window.Init(_uiSystem));
        }
        
        private void OnBackButtonClick()
        {
            _uiSystem.Windows.Hide<SpecialOfferDetailsWindow>(HideInHierarchyStrategy.DefaultCut);
        }
    }
}