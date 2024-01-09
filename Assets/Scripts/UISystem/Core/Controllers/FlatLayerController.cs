using System.Collections.Generic;
using Kha.UI.Extensions;
using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Logic which manipulates UI entities of a flat layer.
    /// /// Creates, holds and executes related commands.
    /// </summary>
    public sealed class FlatLayerController
    {
        private readonly IUIPool _uiPool;
        private readonly UICallbacksBridge _uiCallbacksBridge;
        private readonly List<IUISystemEntity> _entitiesToRemove = new();

        public FlatLayerController(IUIPool uiPool, UICallbacksBridge uiCallbacksBridge)
        {
            _uiPool = uiPool;
            _uiCallbacksBridge = uiCallbacksBridge;
        }

        public T Show<T, TAppearanceAnimation, TDisappearanceAnimation>(FlatLayer layer, IFlatShowStrategy showStrategy) 
            where T : MonoBehaviour, IUISystemEntity 
            where TAppearanceAnimation : UIAnimationBase
            where TDisappearanceAnimation : UIAnimationBase
        {
            var uiEntity = _uiPool.Get<T>();

            if (uiEntity == null)
            {
                UILogger.LogError($"Failed to show UIEntity {typeof(T)} because pool returned null.");
                return default;
            }

            uiEntity.Notify(UIEntityNotification.Created, _uiCallbacksBridge);
            uiEntity.Notify(UIEntityNotification.AppearanceStarted, _uiCallbacksBridge);
            uiEntity.AssignLayer(layer);
            layer.Entities.Add(uiEntity);
            uiEntity.Canvas.sortingLayerID = UIConstants.UISortingLayerId;
            var uiSystemEntityTransform = uiEntity.GameObject.transform;
            var entitiesHolderTransform = layer.EntitiesHolder.transform;
            uiSystemEntityTransform.SetParent(entitiesHolderTransform);
            uiSystemEntityTransform.localScale = Vector3.one;
            
            if (showStrategy.IsFullScreen)
            {                
                uiSystemEntityTransform.localPosition = Vector3.zero;
                uiSystemEntityTransform.SetSiblingIndex(entitiesHolderTransform.childCount - 1);
                uiSystemEntityTransform.TryMakeFullScreen();
            }

            if (typeof(TAppearanceAnimation) != typeof(UIAnimationNone))
            {
                uiEntity.OverrideAppearanceAnimation<TAppearanceAnimation>();
            }
            
            if (typeof(TDisappearanceAnimation) != typeof(UIAnimationNone))
            {
                uiEntity.OverrideDisappearanceAnimation<TDisappearanceAnimation>();
            }
            
            uiEntity.ResolveAppearanceAnimation().Play(() => uiEntity.Notify(UIEntityNotification.AppearanceFinished, 
                _uiCallbacksBridge));
            uiEntity.GameObject.SetActive(true);

            return uiEntity;
        }

        public void HideAllOfType<T, TDisappearanceAnimation>(FlatLayer flatLayer) 
            where T : IUISystemEntity
            where TDisappearanceAnimation : UIAnimationBase
        {
            ReleaseEntities<TDisappearanceAnimation>(flatLayer.Entities.Get<T>());
        }

        public void Hide<TDisappearanceAnimation>(IUISystemEntity uiEntity)
            where TDisappearanceAnimation : UIAnimationBase
        {
            if (typeof(TDisappearanceAnimation) != typeof(UIAnimationNone))
            {
                uiEntity.OverrideDisappearanceAnimation<TDisappearanceAnimation>();
            }
            
            uiEntity.ResolveDisappearanceAnimation().Play(() => OnHidingTransitionEnded(uiEntity));
        }

        public void HideAll<TDisappearanceAnimation>(FlatLayer layer)
            where TDisappearanceAnimation : UIAnimationBase
        {
            ReleaseEntities<TDisappearanceAnimation>(layer.Entities);
        }

        private void ReleaseEntities<TDisappearanceAnimation>(IEnumerable<IUISystemEntity> entitiesToHide)
            where TDisappearanceAnimation : UIAnimationBase
        {
            _entitiesToRemove.Clear();
            _entitiesToRemove.AddRange(entitiesToHide);

            for (var i = 0; i < _entitiesToRemove.Count; i++)
            {
                Hide<TDisappearanceAnimation>(_entitiesToRemove[i]);
            }
        }

        private void OnHidingTransitionEnded(IUISystemEntity uiEntity)
        {
            uiEntity.GameObject.SetActive(false);

            if (uiEntity.CurrentLayer != null && uiEntity.CurrentLayer is FlatLayer flatLayer)
            {
                flatLayer.Entities.Remove(uiEntity);   
            }
            else
            {
                UILogger.LogError($"On UIEntity release it's layer is null or has wrong type: {uiEntity.CurrentLayer}.");
            }
            
            _uiPool.Release(uiEntity);
            uiEntity.Notify(UIEntityNotification.Released, _uiCallbacksBridge);
            uiEntity.Notify(UIEntityNotification.DisappearanceCompleted, _uiCallbacksBridge);
        }
    }
}
