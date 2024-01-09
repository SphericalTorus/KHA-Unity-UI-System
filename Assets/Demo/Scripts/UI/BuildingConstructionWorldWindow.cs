using Kha.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Kha.UI.Demo
{
    public class BuildingConstructionWorldWindow : WorldWindow
    {
        [SerializeField] private Text _buildingProgressLabel;
        [SerializeField] private Button _tipButton;
        [SerializeField] private Button _buildButton;

        private UISystem _uiSystem;
        private UIOwner _uiOwner;

        private float _buildingProgress;

        protected override void OnAppearanceStarted()
        {
            base.OnAppearanceStarted();
            _tipButton.onClick.AddListener(OnTipButtonClick);
            _buildButton.onClick.AddListener(OnBuildButtonClick);
        }

        protected override void OnDisappearanceFinished()
        {
            base.OnDisappearanceFinished();
            _tipButton.onClick.RemoveListener(OnTipButtonClick);
            _buildButton.onClick.RemoveListener(OnBuildButtonClick);
        }

        public void Init(UISystem uiSystem, UIOwner uiOwner)
        {
            _uiSystem = uiSystem;
            _uiOwner = uiOwner;
        }

        private void OnTipButtonClick()
        {
            _uiSystem.WorldWindows.Show<BuildingTipWorldWindow>(_uiOwner, ShowInHierarchyStrategy.OnTop)
                .OnCreated(window => window.Init(_uiSystem, _uiOwner));
        }

        private void OnBuildButtonClick()
        {
            const float progressPerClick = 0.1f;
            _buildingProgress += progressPerClick;
            _buildingProgressLabel.text = $"{(int)(Mathf.Clamp01(_buildingProgress) * 100)} / 100 %";
        }
    }
}

