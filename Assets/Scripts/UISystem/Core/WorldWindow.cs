using UnityEngine;

namespace Kha.UI.Core
{
    public class WorldWindow : UIEntity
    {
        [SerializeField] private RectTransform _rectTransform;

        private bool _isVisible;

        public RectTransform RectTransform => _rectTransform;
        public bool IsPositionInitialized { get; private set; }

        public void SetupVisibility(bool isVisible)
        {
            var wasVisible = _isVisible;
            _isVisible = isVisible;

            if (_isVisible != wasVisible)
            {
                if (_isVisible)
                {
                    Canvas.enabled = true;
                    OnBecomeVisible();
                }
                else
                {
                    Canvas.enabled = false;
                    OnBecomeInvisible();
                }
            }
        }

        public void OnPositionInitialized()
        {
            IsPositionInitialized = true;
        }

        protected override void OnAppearanceStarted()
        {
            base.OnAppearanceStarted();
            IsPositionInitialized = false;
        }

        protected virtual void OnBecomeVisible()
        {
            // override me if needed
        }

        protected virtual void OnBecomeInvisible()
        {
            // override me if needed
        }
    }
}
