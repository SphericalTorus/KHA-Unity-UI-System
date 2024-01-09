using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Part of the UI system responsible for windows logic.
    /// </summary>
    public sealed class WindowsSystem : IWindowsSystem
    {
        private readonly HierarchicalLayer _windowsLayer;
        private readonly HierarchicalLayerController _hierarchicalLayerController;

        public HierarchicalLayerController Controller => _hierarchicalLayerController;
        
        public WindowsSystem(UIEntitiesHolder windowsHolder, IUIPool uiPool, UICallbacksBridge uiCallbacksBridge)
        {
            _windowsLayer = new HierarchicalLayer(windowsHolder);
            _hierarchicalLayerController = new HierarchicalLayerController(uiPool, uiCallbacksBridge);
        }
        
        public IShowUIEntityCommand<T> Show<T>(IShowInHierarchyStrategy showStrategy) 
            where T : MonoBehaviour, IUISystemEntity
        {
            return _hierarchicalLayerController.ShowEntity<T>(
                _windowsLayer, showStrategy);
        }
        
        public IShowUIEntityCommand<T> Show<T>() 
            where T : MonoBehaviour, IUISystemEntity
        {
            return _hierarchicalLayerController.ShowEntity<T>(
                _windowsLayer, ShowInHierarchyStrategy.ReleaseAllCut);
        }

        public void Hide<T>(IHideInHierarchyStrategy hideStrategy) where T : MonoBehaviour, IUISystemEntity
        {
            _hierarchicalLayerController.HideEntity<T>(_windowsLayer, hideStrategy);
        }
        
        public void Hide<T>() where T : MonoBehaviour, IUISystemEntity
        {
            _hierarchicalLayerController.HideEntity<T>(_windowsLayer, HideInHierarchyStrategy.DefaultCut);
        }

        public void Hide(UIEntity uiEntity, IHideInHierarchyStrategy hideStrategy)
        {
            _hierarchicalLayerController.HideEntity(uiEntity, _windowsLayer, hideStrategy);
        }
        
        public void Hide(UIEntity uiEntity)
        {
            _hierarchicalLayerController.HideEntity(uiEntity, _windowsLayer, HideInHierarchyStrategy.DefaultCut);
        }
        
        public T GetWindow<T>() where T : MonoBehaviour, IUISystemEntity
        {
            return _windowsLayer.Hierarchy.FindNode<T>(out var depth);
        }
        
        public IUISystemEntity GetWindowOnTop()
        {
            return _windowsLayer.Hierarchy.GetLeaf();
        }

        public bool HasPendingCommands()
        {
            return !_hierarchicalLayerController.HasScheduledCommands;
        }
    }
}