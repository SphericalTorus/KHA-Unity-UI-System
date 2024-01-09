using System;
using Kha.UI.Extensions;
using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Command which shows a UI entity.
    /// </summary>
    /// <typeparam name="T">Type of a showing UI entity.</typeparam>
    public sealed class ShowUIEntityCommand<T> : UICommand, IShowUIEntityCommand<T>
        where T : MonoBehaviour, IUISystemEntity
    {
        private readonly IUIPool _uiPool;
        private readonly HierarchicalLayer _layer;
        private readonly UICallbacksBridge _uiCallbacksBridge;
        private bool _overrideAppearanceAnimation;
        private Type _overridenAppearanceAnimationType;
        private bool _overrideDisappearanceAnimation;
        private Type _overridenDisappearanceAnimationType;
        private bool _isCompleted;
        private Action<T> _onCreated;
        private IUISystemEntity _showingEntity;

        public ShowUIEntityCommand(IUIPool uiPool, HierarchicalLayer layer, UICallbacksBridge uiCallbacksBridge)
        {
            _uiPool = uiPool;
            _layer = layer;
            _uiCallbacksBridge = uiCallbacksBridge;
        }

        public override void Execute()
        {
            _showingEntity = _uiPool.Get<T>();
            _showingEntity.Notify(UIEntityNotification.Created, _uiCallbacksBridge);
            _onCreated?.Invoke(_showingEntity as T);

            if (_showingEntity == null)
            {
                UILogger.LogError($"Failed to show UIEntity {typeof(T)} because pool returned null.");
                _isCompleted = true;
                return;
            }

            _showingEntity.Notify(UIEntityNotification.AppearanceStarted, _uiCallbacksBridge);
            _showingEntity.AssignLayer(_layer);
            _layer.Hierarchy.AddLeaf(_showingEntity);
            _showingEntity.Canvas.overrideSorting = true;
            _showingEntity.Canvas.sortingLayerID = UIConstants.UISortingLayerId;
            _showingEntity.Canvas.sortingOrder = _layer.Hierarchy.GetDepth() * UIConstants.CanvasSortingLayerStep;
            var uiSystemEntityTransform = _showingEntity.GameObject.transform;
            var entitiesHolderTransform = _layer.EntitiesHolder.transform;
            uiSystemEntityTransform.SetParent(entitiesHolderTransform);
            uiSystemEntityTransform.localScale = Vector3.one;
            uiSystemEntityTransform.localPosition = Vector3.zero;
            uiSystemEntityTransform.SetSiblingIndex(entitiesHolderTransform.childCount - 1);
            uiSystemEntityTransform.TryMakeFullScreen();

            if (_overrideAppearanceAnimation)
            {
                _showingEntity.OverrideAppearanceAnimation(_overridenAppearanceAnimationType);
            }

            if (_overrideDisappearanceAnimation)
            {
                _showingEntity.OverrideDisappearanceAnimation(_overridenDisappearanceAnimationType);
            }

            _showingEntity.ResolveAppearanceAnimation().Play(FinalizeShow);         
            _showingEntity.GameObject.SetActive(true);
        }

        public override bool IsCompleted()
        {
            return _isCompleted;
        }

        private void FinalizeShow()
        {
            _showingEntity.Notify(UIEntityNotification.AppearanceFinished, _uiCallbacksBridge);
            _isCompleted = true;
        }

        IShowUIEntityCommand<T> IShowUIEntityCommand<T>.AppearWith<TOverridenAppearanceAnimation>()
        {
            _overrideAppearanceAnimation = true;
            _overridenAppearanceAnimationType = typeof(TOverridenAppearanceAnimation);
            return this;
        }
        
        IShowUIEntityCommand<T> IShowUIEntityCommand<T>.DisappearWith<TOverridenDisappearanceAnimation>()
        {
            _overrideDisappearanceAnimation = true;
            _overridenDisappearanceAnimationType = typeof(TOverridenDisappearanceAnimation);
            return this;
        }

        IShowUIEntityCommand<T> IShowUIEntityCommand<T>.OnCreated(Action<T> onCreated)
        {
            _onCreated = onCreated;
            return this;
        }
    }
}
