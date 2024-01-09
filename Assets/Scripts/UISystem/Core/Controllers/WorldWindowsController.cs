using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Logic which manipulates UI entities attached to world objects. Each world object has it's own hierarchy.
    /// Creates, holds and executes related commands.
    /// </summary>
    public sealed class WorldWindowsController
    {
        private readonly HierarchicalLayerController _hierarchicalLayerController;
        private readonly UIEntitiesHolder _entitiesHolder;
        private readonly Dictionary<UIOwner, HierarchicalLayer> _layersByOwner = new();

        private Camera _worldCamera;
        private Camera _uiCamera;

        public WorldWindowsController(HierarchicalLayerController hierarchicalLayerController,
            UIEntitiesHolder entitiesHolder)
        {
            _hierarchicalLayerController = hierarchicalLayerController;
            _entitiesHolder = entitiesHolder;
        }

        public void InjectWorldCamera(Camera camera)
        {
            _worldCamera = camera;
        }

        /// <param name="camera">null is expected in case of overlaying mode set on canvas</param>
        public void InjectUICamera(Camera camera)
        {
            _uiCamera = camera;
        }

        public void Update()
        {
            _hierarchicalLayerController.Update();
            
            if (_worldCamera)
            {
                UpdateWindowsPositions();
            }
        }

        public IShowUIEntityCommand<T> Show<T>(UIOwner uiOwner, IShowInHierarchyStrategy showStrategy)
            where T : WorldWindow
        {
            if (!_layersByOwner.ContainsKey(uiOwner))
            {
                _layersByOwner.Add(uiOwner, new HierarchicalLayer(_entitiesHolder));
            }

            return _hierarchicalLayerController.ShowEntity<T>(
                _layersByOwner[uiOwner], showStrategy);
        }

        public void Hide<T>(UIOwner uiOwner, IHideInHierarchyStrategy hideStrategy) where T : WorldWindow
        {
            HideInternal(uiOwner, () =>
                _hierarchicalLayerController.HideEntity<T>(_layersByOwner[uiOwner], hideStrategy));
        }
        
        public void Hide(UIOwner uiOwner, IUISystemEntity uiEntity, IHideInHierarchyStrategy hideStrategy)
        {
            HideInternal(uiOwner, () =>
                _hierarchicalLayerController.HideEntity(uiEntity, _layersByOwner[uiOwner], hideStrategy));
        }

        public void HideAll()
        {
            foreach (var layerByObject in _layersByOwner)
            {
                var layer = layerByObject.Value;

                foreach (var uiEntity in layerByObject.Value.Hierarchy)
                {
                    _hierarchicalLayerController.HideEntity(uiEntity, layer, HideInHierarchyStrategy.DefaultCut);
                }
            }
                
            _layersByOwner.Clear();
        }

        public bool HasWindow<T>(UIOwner uiOwner) where T : WorldWindow
        {
            return _layersByOwner.ContainsKey(uiOwner) &&
                _layersByOwner[uiOwner].Hierarchy.HasNode<T>();
        }

        public void ForceUpdateWindowsPositions()
        {
            UpdateWindowsPositions(true);
        }

        private void UpdateWindowsPositions(bool force = false)
        {
            foreach (var layerByObject in _layersByOwner)
            {
                var owner = layerByObject.Key;

                foreach (var uiSystemEntity in layerByObject.Value.Hierarchy)
                {
                    var worldWindow = (WorldWindow)uiSystemEntity;
                    worldWindow.SetupVisibility(owner.IsVisible());

                    if (!worldWindow.IsPositionInitialized || force)
                    {
                        UpdateWindowPosition(owner, worldWindow);
                        worldWindow.OnPositionInitialized();
                    }
                    else if (owner.IsVisible())
                    {
                        UpdateWindowPosition(owner, worldWindow);
                    }
                }
            }
        }

        private void HideInternal(UIOwner uiOwner, Action hideAction)
        {
            if (!_layersByOwner.ContainsKey(uiOwner))
            {
                return;
            }

            hideAction?.Invoke();
            
            if (_layersByOwner[uiOwner].Hierarchy.GetDepth() == 0)
            {
                _layersByOwner.Remove(uiOwner);
            }
        }

        private void UpdateWindowPosition(UIOwner owner, WorldWindow gameObjectWindow)
        {
            var windowAnchoredPosition = GetViewAnchoredPosition(owner.transform.position);
            SetViewPosition(gameObjectWindow.RectTransform, windowAnchoredPosition, owner.UIOffset);
        }

        private Vector3 GetViewAnchoredPosition(Vector3 worldPosition)
        {
            var screenPos = _worldCamera.WorldToScreenPoint(worldPosition);
            var screenPos2D = new Vector2(screenPos.x, screenPos.y);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _entitiesHolder.RectTransform, screenPos2D, _uiCamera, out var anchoredPos);

            return anchoredPos;
        }

        private void SetViewPosition(RectTransform rectTransform, Vector3 position, Vector3 offset)
        {
            var centerValue = 0.5f * Vector3.one;
            rectTransform.anchorMin = centerValue;
            rectTransform.anchorMax = centerValue;
            rectTransform.pivot = centerValue;
            rectTransform.anchoredPosition = position + offset;
        }
    }
}
