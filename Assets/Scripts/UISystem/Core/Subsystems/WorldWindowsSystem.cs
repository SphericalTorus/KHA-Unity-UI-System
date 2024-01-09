namespace Kha.UI.Core
{
    /// <summary>
    /// Part of the UI system responsible for world windows.
    /// </summary>
    public sealed class WorldWindowsSystem : IWorldWindowsSystem
    {
        private readonly WorldWindowsController _worldWindowsController;

        public WorldWindowsController Controller => _worldWindowsController;
        
        public WorldWindowsSystem(UIEntitiesHolder worldWindowsHolder, IUIPool uiPool, UICallbacksBridge uiCallbacksBridge)
        {
            var hierarchicalLayerController = new HierarchicalLayerController(uiPool, uiCallbacksBridge);
            _worldWindowsController = new WorldWindowsController(hierarchicalLayerController, worldWindowsHolder);
        }
        
        public IShowUIEntityCommand<T> Show<T>(UIOwner uiOwner, IShowInHierarchyStrategy showStrategy) where T : WorldWindow
        {
            return _worldWindowsController.Show<T>(uiOwner, showStrategy);
        }
        
        public IShowUIEntityCommand<T> Show<T>(UIOwner uiOwner) where T : WorldWindow
        {
            return _worldWindowsController.Show<T>(
                uiOwner, ShowInHierarchyStrategy.ReleaseAllCut);
        }

        public bool IsWorldWindowShown<T>(UIOwner uiOwner) where T : WorldWindow
        {
            return _worldWindowsController.HasWindow<T>(uiOwner);
        }
        
        public void Hide<T>(UIOwner uiOwner, IHideInHierarchyStrategy hideStrategy) where T : WorldWindow
        {
            _worldWindowsController.Hide<T>(uiOwner, hideStrategy);
        }
        
        public void Hide<T>(UIOwner uiOwner) where T : WorldWindow
        {
            _worldWindowsController.Hide<T>(uiOwner, HideInHierarchyStrategy.DefaultCut);
        }
        
        public void Hide(UIEntity window, UIOwner uiOwner, IHideInHierarchyStrategy hideStrategy)
        {
            _worldWindowsController.Hide(uiOwner, window, hideStrategy);
        }
        
        public void Hide(UIEntity window, UIOwner uiOwner)
        {
            _worldWindowsController.Hide(uiOwner, window, HideInHierarchyStrategy.DefaultCut);
        }

        public void HideAll()
        {
            _worldWindowsController.HideAll();
        }

        public void ForceUpdatePositions()
        {
            _worldWindowsController.ForceUpdateWindowsPositions();
        }
    }
}