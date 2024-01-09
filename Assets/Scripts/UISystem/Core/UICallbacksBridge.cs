using System;

namespace Kha.UI.Core
{
    public sealed class UICallbacksBridge : IUICallbacksBridge
    {
        public event Action<UIEntity> UIEntityAppearanceFinished;
        public event Action<UIEntity> UIEntityAppearanceStarted;
        public event Action<UIEntity> UIEntityDisappearanceFinished;
        public event Action<UIEntity> UIEntityDisappearanceStarted;
        public event Action<UIEntity> UIEntityReleased;

        public void OnUIEntityAppearanceStarted<T>(T window) where T : UIEntity
        {
            UIEntityAppearanceStarted?.Invoke(window);
        }

        public void OnUIEntityAppearanceFinished<T>(T window) where T : UIEntity
        {
            UIEntityAppearanceFinished?.Invoke(window);
        }

        public void OnUIEntityReleased<T>(T window) where T : UIEntity
        {
            UIEntityReleased?.Invoke(window);
        }
        
        public void OnUIEntityDisappearanceStarted<T>(T window) where T : UIEntity
        {
            UIEntityDisappearanceStarted?.Invoke(window);
        }
        
        public void OnUIEntityDisappearanceFinished<T>(T window) where T : UIEntity
        {
            UIEntityDisappearanceFinished?.Invoke(window);
        }
    }
}
