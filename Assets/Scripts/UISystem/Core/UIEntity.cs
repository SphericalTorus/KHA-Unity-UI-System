using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kha.UI.Core
{
    public abstract class UIEntity : MonoBehaviour, IUISystemEntity
    {
        [SerializeField] private bool _prewarm;
        [SerializeField] private bool _has3dObject;
        [SerializeField] protected Canvas _canvas;
        [SerializeField] private List<UIAnimationBase> _appearanceAnimations;
        [SerializeField] private List<UIAnimationBase> _disappearanceAnimations;

        private Layer _currentLayer;
        private UIAnimationBase _overridenAppearanceAnimation;
        private UIAnimationBase _overridenDisappearanceAnimation;
        private UIAnimationBase _defaultDisappearanceAnimation;
        private UIAnimationBase _defaultAppearanceAnimation;
        private bool _isShown;
        
        public bool Prewarm => _prewarm;
        public Canvas Canvas => _canvas;
        GameObject IUISystemEntity.GameObject => gameObject;
        bool IUISystemEntity.IsShown => _isShown;
        bool IUISystemEntity.Has3dObject => _has3dObject;
        Layer IUISystemEntity.CurrentLayer => _currentLayer;

        void IUISystemEntity.Notify(UIEntityNotification notification, UICallbacksBridge uiCallbacksBridge)
        {
            switch (notification)
            {
                case UIEntityNotification.Created:
                    break;
                case UIEntityNotification.AppearanceStarted:
                    _isShown = true;
                    OnAppearanceStarted();
                    uiCallbacksBridge.OnUIEntityAppearanceStarted(this);
                    break;
                case UIEntityNotification.AppearanceFinished:
                    _overridenAppearanceAnimation = null;
                    OnAppearanceFinished();
                    uiCallbacksBridge.OnUIEntityAppearanceFinished(this);
                    break;
                case UIEntityNotification.DisappearanceStarted:
                    OnDisappearanceStarted();
                    uiCallbacksBridge.OnUIEntityDisappearanceStarted(this);
                    break;
                case UIEntityNotification.DisappearanceCompleted:
                    _overridenDisappearanceAnimation = null;
                    OnDisappearanceFinished();
                    uiCallbacksBridge.OnUIEntityDisappearanceFinished(this);
                    _isShown = false;
                    break;
                case UIEntityNotification.Released:
                    uiCallbacksBridge.OnUIEntityReleased(this);
                    _currentLayer = null;
                    break;
                default:
                    UILogger.LogWarning($"UI entity {gameObject.name} doesn't support notification type {notification}");
                    break;
            }
        }

        void IUISystemEntity.AssignLayer(Layer layer)
        {
            _currentLayer = layer;
        }

        UIAnimationBase IUISystemEntity.ResolveAppearanceAnimation()
        {
            if (_overridenAppearanceAnimation != null)
            {
                return _overridenAppearanceAnimation;
            }

            if (_defaultAppearanceAnimation != null)
            {
                return _defaultAppearanceAnimation;
            }

            _defaultAppearanceAnimation = _appearanceAnimations.FirstOrDefault();

            if (_defaultAppearanceAnimation != null)
            {
                return _defaultAppearanceAnimation;
            }

            var emptyAnimation = GetComponent<UIAnimationEmpty>();
            _defaultAppearanceAnimation = emptyAnimation != null
                ? emptyAnimation
                : gameObject.AddComponent<UIAnimationEmpty>();
            
            return _defaultAppearanceAnimation;
        }

        UIAnimationBase IUISystemEntity.ResolveDisappearanceAnimation()
        {
            if (_overridenDisappearanceAnimation != null)
            {
                return _overridenDisappearanceAnimation;
            }

            if (_defaultDisappearanceAnimation != null)
            {
                return _defaultDisappearanceAnimation;
            }

            _defaultDisappearanceAnimation = _disappearanceAnimations.FirstOrDefault();

            if (_defaultDisappearanceAnimation != null)
            {
                return _defaultDisappearanceAnimation;
            }

            var emptyAnimation = GetComponent<UIAnimationNone>();
            _defaultDisappearanceAnimation = emptyAnimation != null
                ? emptyAnimation
                : gameObject.AddComponent<UIAnimationNone>();

            return _defaultDisappearanceAnimation;
        }

        void IUISystemEntity.OverrideAppearanceAnimation<T>()
        {
            var newAnimation = _appearanceAnimations.Find(t => t is T);
            OverrideAppearanceAnimation(typeof(T), newAnimation);
        }

        void IUISystemEntity.OverrideAppearanceAnimation(Type appearanceAnimationType)
        {
            var newAnimation = _appearanceAnimations.Find(t => t.GetType() == appearanceAnimationType);
            OverrideAppearanceAnimation(appearanceAnimationType, newAnimation);
        }

        void IUISystemEntity.OverrideDisappearanceAnimation<T>()
        {
            var newAnimation = _disappearanceAnimations.Find(t => t is T);
            OverrideDisappearanceAnimation(typeof(T), newAnimation);
        }
        
        void IUISystemEntity.OverrideDisappearanceAnimation(Type disappearanceAnimationType)
        {
            var newAnimation = _disappearanceAnimations.Find(t => t.GetType() == disappearanceAnimationType);
            OverrideDisappearanceAnimation(disappearanceAnimationType, newAnimation);
        }

        protected virtual void OnAppearanceStarted()
        {
            // override if necessary
        }

        protected virtual void OnDisappearanceStarted()
        {
            // override if necessary
        }

        protected virtual void OnAppearanceFinished()
        {
            // override if necessary
        }

        protected virtual void OnDisappearanceFinished()
        {
            // override if necessary
        }

        private void OverrideAppearanceAnimation(Type desiredAnimation, UIAnimationBase foundAnimation)
        {
            if (foundAnimation == null)
            {
                UILogger.LogError($"UIEntity.OverrideAppearanceAnimation(): " +
                    $"can't find animation of type {desiredAnimation} on object {gameObject.name}");
            }

            _overridenAppearanceAnimation = foundAnimation;
        }
        
        private void OverrideDisappearanceAnimation(Type desiredAnimation, UIAnimationBase foundAnimation)
        {
            if (foundAnimation == null)
            {
                UILogger.LogError($"UIEntity.OverrideDisappearanceAnimation(): " +
                    $"can't find animation of type {desiredAnimation} on object {gameObject.name}");
            }

            _overridenDisappearanceAnimation = foundAnimation;
        }
    }
}
