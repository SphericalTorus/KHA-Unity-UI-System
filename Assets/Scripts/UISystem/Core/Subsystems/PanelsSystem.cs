using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Part of the UI system responsible for panels logic.
    /// </summary>
    public sealed class PanelsSystem : IPanelsSystem
    {
        private readonly FlatLayerController _flatLayerController;
        private readonly FlatLayer _overlayingPanelsLayer;
        private readonly FlatLayer _underlyingPanelsLayer;
        
        public PanelsSystem(UIEntitiesHolder underlyingPanelsHolder, UIEntitiesHolder overlayingPanelsHolder,
            IUIPool uiPool, UICallbacksBridge uiCallbacksBridge)
        {
            _underlyingPanelsLayer = new FlatLayer(underlyingPanelsHolder);
            _overlayingPanelsLayer = new FlatLayer(overlayingPanelsHolder);
            _flatLayerController = new FlatLayerController(uiPool, uiCallbacksBridge);
        }
        
        public T Show<T, TAppearanceAnimation>(IFlatShowStrategy showStrategy) 
            where T : MonoBehaviour, IUISystemEntity
            where TAppearanceAnimation : UIAnimationBase
        {
            var layer = DefineLayer(showStrategy.IsOverlaying);
            return _flatLayerController.Show<T, TAppearanceAnimation, UIAnimationNone>(layer, showStrategy);
        }
        
        public T Show<T, TAppearanceAnimation, TDisappearanceAnimation>(IFlatShowStrategy showStrategy) 
            where T : MonoBehaviour, IUISystemEntity
            where TAppearanceAnimation : UIAnimationBase
            where TDisappearanceAnimation : UIAnimationBase
        {
            var layer = DefineLayer(showStrategy.IsOverlaying);
            return _flatLayerController.Show<T, TAppearanceAnimation, TDisappearanceAnimation>(layer, showStrategy);
        }
        
        public T Show<T>(IFlatShowStrategy showStrategy) where T : MonoBehaviour, IUISystemEntity
        {
            var layer = DefineLayer(showStrategy.IsOverlaying);
            return _flatLayerController.Show<T, UIAnimationNone, UIAnimationNone>(layer, showStrategy);
        }
        
        public T Show<T>() where T : MonoBehaviour, IUISystemEntity
        {
            var showStrategy = FlatShowStrategy.Default;
            var layer = DefineLayer(showStrategy.IsOverlaying);
            return _flatLayerController.Show<T, UIAnimationNone, UIAnimationNone>(layer, showStrategy);
        }
        
        public T Show<T, TAppearanceAnimation>() 
            where T : MonoBehaviour, IUISystemEntity
            where TAppearanceAnimation : UIAnimationBase
        {
            var showStrategy = FlatShowStrategy.Default;
            var layer = DefineLayer(showStrategy.IsOverlaying);
            return _flatLayerController.Show<T, TAppearanceAnimation, UIAnimationNone>(layer, showStrategy);
        }
        
        public T Show<T, TAppearanceAnimation, TDisappearanceAnimation>() 
            where T : MonoBehaviour, IUISystemEntity
            where TAppearanceAnimation : UIAnimationBase
            where TDisappearanceAnimation : UIAnimationBase
        {
            var showStrategy = FlatShowStrategy.Default;
            var layer = DefineLayer(showStrategy.IsOverlaying);
            return _flatLayerController.Show<T, TAppearanceAnimation, TDisappearanceAnimation>(layer, showStrategy);
        }
        
        public void HideAll<T, TDisappearanceAnimation>(bool inOverlayingLayer) 
            where T : IUISystemEntity
            where TDisappearanceAnimation : UIAnimationBase
        {
            _flatLayerController.HideAllOfType<T, TDisappearanceAnimation>(DefineLayer(inOverlayingLayer));
        }
        
        public void HideAll<T>(bool inOverlayingLayer) where T : IUISystemEntity
        {
            _flatLayerController.HideAllOfType<T, UIAnimationNone>(DefineLayer(inOverlayingLayer));
        }

        public void Hide<TDisappearanceAnimation>(IUISystemEntity uiEntity)
            where TDisappearanceAnimation : UIAnimationBase
        {
            _flatLayerController.Hide<TDisappearanceAnimation>(uiEntity);
        }
        
        public void Hide(IUISystemEntity uiEntity)
        {
            _flatLayerController.Hide<UIAnimationNone>(uiEntity);
        }

        public bool IsShown<T>(bool overlaying) where T : IUISystemEntity
        {
            return DefineLayer(overlaying).Entities.HasEntity<T>();
        }

        private FlatLayer DefineLayer(bool overlaying)
        {
            return overlaying ? _overlayingPanelsLayer : _underlyingPanelsLayer;
        }
    }
}