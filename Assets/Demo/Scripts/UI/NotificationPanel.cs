using Kha.UI.Core;
using UnityEngine;

namespace Kha.UI.Demo
{
    public class NotificationPanel : UIEntity
    {
        private const float VisibilityDuration = 1.5f;
        
        private UISystem _uiSystem;
        private float _timeToHide;
        
        public void Init(UISystem uiSystem)
        {
            _uiSystem = uiSystem;
            _timeToHide = Time.time + VisibilityDuration;
        }

        private void Update()
        {
            if (Time.time >= _timeToHide)
            {
                _timeToHide = float.MaxValue;
                _uiSystem.Panels.Hide(this);
            }
        }
    }
}
