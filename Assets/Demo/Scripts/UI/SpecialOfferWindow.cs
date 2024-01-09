using Kha.UI.Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Kha.UI.Demo
{
    public class SpecialOfferWindow : UIEntity
    {
        [SerializeField] private Button _detailsButton;
        [SerializeField] private Button _closeButton;

        private UISystem _uiSystem;

        protected override void OnAppearanceStarted()
        {
            _detailsButton.onClick.AddListener(OnDetailsButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        protected override void OnDisappearanceFinished()
        {
            _detailsButton.onClick.RemoveListener(OnDetailsButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        public void Init(UISystem uiSystem)
        {
            _uiSystem = uiSystem;
        }

        private void OnDetailsButtonClick()
        {
            _uiSystem.Windows.Show<SpecialOfferDetailsWindow>(ShowInHierarchyStrategy.HideAll)
                .OnCreated(window => window.Init(_uiSystem));
        }
        
        private void OnCloseButtonClick()
        {
            _uiSystem.Windows.Hide<SpecialOfferWindow>();
        }
    }
}