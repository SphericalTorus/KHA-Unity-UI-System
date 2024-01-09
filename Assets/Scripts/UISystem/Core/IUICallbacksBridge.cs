using System;

namespace Kha.UI.Core
{
    public interface IUICallbacksBridge
    {
        public event Action<UIEntity> UIEntityAppearanceFinished;
        public event Action<UIEntity> UIEntityAppearanceStarted;
        public event Action<UIEntity> UIEntityDisappearanceFinished;
        public event Action<UIEntity> UIEntityDisappearanceStarted;
        public event Action<UIEntity> UIEntityReleased;
    }
}
