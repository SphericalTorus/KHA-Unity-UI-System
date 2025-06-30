using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Outer interface of the UI system part, which is responsible for panels logic.
    /// </summary>
    public interface IPanelsSystem
    {
        /// <summary>
        /// Shows a panel with the specified strategy and overrides an appearance animation.
        /// </summary>
        /// <param name="showStrategy">How a panel must be shown.</param>
        /// <typeparam name="T">Type of showing panel.</typeparam>
        /// <typeparam name="TAppearanceAnimation">Type of appearance animation, which will override default one.</typeparam>
        /// <returns>Shown panel.</returns>
        T Show<T, TAppearanceAnimation>(IFlatShowStrategy showStrategy)
            where T : MonoBehaviour, IUISystemEntity
            where TAppearanceAnimation : UIAnimationBase;
        
        /// <summary>
        /// Shows a panel with the specified strategy and overrides an appearance and disappearance animations.
        /// </summary>
        /// <param name="showStrategy">How a panel must be shown.</param>
        /// <typeparam name="T">Type of showing panel.</typeparam>
        /// <typeparam name="TAppearanceAnimation">Type of appearance animation, which will override default one.</typeparam>
        /// <typeparam name="TDisappearanceAnimation">Type of Disappearance animation, which will override default one.</typeparam>
        /// <returns>Shown panel.</returns>
        T Show<T, TAppearanceAnimation, TDisappearanceAnimation>(IFlatShowStrategy showStrategy)
            where T : MonoBehaviour, IUISystemEntity
            where TAppearanceAnimation : UIAnimationBase
            where TDisappearanceAnimation : UIAnimationBase;
        
        /// <summary>
        /// Shows a panel with the specified strategy.
        /// </summary>
        /// <param name="showStrategy">How a panel must be shown.</param>
        /// <typeparam name="T">Type of showing panel.</typeparam>
        /// <returns>Shown panel.</returns>
        T Show<T>(IFlatShowStrategy showStrategy)
            where T : MonoBehaviour, IUISystemEntity;
        
        /// <summary>
        /// Shows a panel with the default strategy.
        /// </summary>
        /// <typeparam name="T">Type of showing panel.</typeparam>
        /// <returns>Shown panel.</returns>
        T Show<T>() where T : MonoBehaviour, IUISystemEntity;
        
        /// <summary>
        /// Shows a panel with the default strategy and overrides an appearance animation.
        /// </summary>
        /// <typeparam name="T">Type of showing panel.</typeparam>
        /// <typeparam name="TAppearanceAnimation">Type of appearance animation, which will override default one.</typeparam>
        /// <returns>Shown panel.</returns>
        T Show<T, TAppearanceAnimation>()
            where T : MonoBehaviour, IUISystemEntity
            where TAppearanceAnimation : UIAnimationBase;
        
        /// <summary>
        /// Shows a panel with the default strategy and overrides an appearance and disappearance animations.
        /// </summary>
        /// <typeparam name="T">Type of showing panel.</typeparam>
        /// <typeparam name="TAppearanceAnimation">Type of appearance animation, which will override default one.</typeparam>
        /// <typeparam name="TDisappearanceAnimation">Type of disappearance animation, which will override default one.</typeparam>
        /// <returns></returns>
        T Show<T, TAppearanceAnimation, TDisappearanceAnimation>()
            where T : MonoBehaviour, IUISystemEntity
            where TAppearanceAnimation : UIAnimationBase
            where TDisappearanceAnimation : UIAnimationBase;
        
        /// <summary>
        /// Hides all panels of the specified type with custom disappearance animations.
        /// </summary>
        /// <param name="inOverlayingLayer">If true - method applies to overlaying panels, otherwise - to underlying.</param>
        /// <typeparam name="T">Type of hiding panels.</typeparam>
        /// <typeparam name="TDisappearanceAnimation">Type of custom disappearance animation.</typeparam>
        void HideAll<T, TDisappearanceAnimation>(bool inOverlayingLayer)
            where T : IUISystemEntity
            where TDisappearanceAnimation : UIAnimationBase;
        
        /// <summary>
        /// Hides all panels of the specified type.
        /// </summary>
        /// <param name="inOverlayingLayer">If true - method applies to overlaying panels, otherwise - to underlying.</param>
        /// <typeparam name="T">Type of hiding panels.</typeparam>
        void HideAll<T>(bool inOverlayingLayer) 
            where T : IUISystemEntity;
        
        /// <summary>
        /// Hides a panel with custom disappearance animation.
        /// </summary>
        /// <param name="uiEntity">Panel to hide.</param>
        /// <typeparam name="TDisappearanceAnimation">Type of custom disappearance animation.</typeparam>
        void Hide<TDisappearanceAnimation>(IUISystemEntity uiEntity)
            where TDisappearanceAnimation : UIAnimationBase;
        
        /// <summary>
        /// Hides a panel.
        /// </summary>
        /// <param name="uiEntity">Panel to hide.</param>
        void Hide(IUISystemEntity uiEntity);
        
        /// <summary>
        /// Checks if panel of the specified type is shown.
        /// </summary>
        /// <param name="overlaying">Is target panel overlaying or underlying.</param>
        /// <typeparam name="T">Type of checking panel.</typeparam>
        /// <returns>True if panel is shown, false - otherwise.</returns>
        bool IsShown<T>(bool overlaying) 
            where T : IUISystemEntity;
    }
}