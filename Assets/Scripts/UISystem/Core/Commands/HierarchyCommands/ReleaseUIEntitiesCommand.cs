namespace Kha.UI.Core
{
    /// <summary>
    /// Command which releases few UI entities.
    /// </summary>
    public sealed class ReleaseUIEntitiesCommand : DeactivateEntitiesUICommand
    {
        public ReleaseUIEntitiesCommand(HierarchicalEntitiesStorage hierarchy, 
            IUIPool uiPool,bool playLeafHideAnimation, int depthToStop, UICallbacksBridge uiCallbacksBridge) 
            : base(hierarchy, uiPool, playLeafHideAnimation, depthToStop, uiCallbacksBridge)
        {
        }

        protected override bool Release()
        {
            return true;
        }
    }
}
