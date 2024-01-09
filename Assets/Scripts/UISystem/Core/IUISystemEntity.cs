using System;
using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Makes some methods in UIEntity visible only for UISystem.
    /// </summary>
    public interface IUISystemEntity
    {
        GameObject GameObject { get; }
        bool Prewarm { get; }
        bool Has3dObject { get; }
        Canvas Canvas { get; }
        bool IsShown { get; }
        Layer CurrentLayer { get; }

        void Notify(UIEntityNotification notification, UICallbacksBridge uiCallbacksBridge);
        void AssignLayer(Layer layer);
        UIAnimationBase ResolveAppearanceAnimation();
        UIAnimationBase ResolveDisappearanceAnimation();
        void OverrideAppearanceAnimation<T>() where T : UIAnimationBase;
        void OverrideAppearanceAnimation(Type appearanceAnimationType);
        void OverrideDisappearanceAnimation<T>() where T : UIAnimationBase;
        void OverrideDisappearanceAnimation(Type disappearanceAnimationType);
    }
}