namespace Kha.UI.Core
{
    /// <summary>
    /// Command which hides few UI entities.
    /// </summary>
    public sealed class HideUIEntitiesCommand : DeactivateEntitiesUICommand
    {
        public HideUIEntitiesCommand(HierarchicalEntitiesStorage hierarchy, 
            IUIPool uiPool, bool playLeafHideAnimation, int depthToStop, UICallbacksBridge uiCallbacksBridge) 
            : base(hierarchy, uiPool, playLeafHideAnimation, depthToStop, uiCallbacksBridge)
        {
        }

        protected override bool Release()
        {
            return false;
        }
    }
}
