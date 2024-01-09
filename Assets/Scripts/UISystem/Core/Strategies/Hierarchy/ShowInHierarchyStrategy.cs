namespace Kha.UI.Core
{
    public struct ShowInHierarchyStrategy : IShowInHierarchyStrategy
    {
        public ActionOnOthers ActionOnOthers { get; }
        public bool PreserveCurrentRoot { get; }
        public bool PlayPreviousEntityDisappearanceAnimation { get; }
        
        public static IShowInHierarchyStrategy HideAll => 
            new ShowInHierarchyStrategy(ActionOnOthers.HideAll, false, true);
        public static IShowInHierarchyStrategy HideAllCut => 
            new ShowInHierarchyStrategy(ActionOnOthers.HideAll, false, false);
        public static IShowInHierarchyStrategy OnTop =>
            new ShowInHierarchyStrategy(ActionOnOthers.None, false, false);
        public static IShowInHierarchyStrategy ReleaseAll =>
            new ShowInHierarchyStrategy(ActionOnOthers.ReleaseAll, false, true);
        public  static IShowInHierarchyStrategy ReleaseAllCut => 
            new ShowInHierarchyStrategy(ActionOnOthers.ReleaseAll, false, false);
        public  static IShowInHierarchyStrategy ReleaseAllExceptRootCut => 
            new ShowInHierarchyStrategy(ActionOnOthers.ReleaseAll, true, false);

        public ShowInHierarchyStrategy(ActionOnOthers actionOnOthers, bool preserveCurrentRoot, bool playLeafDisappearanceAnimation)
        {
            ActionOnOthers = actionOnOthers;
            PreserveCurrentRoot = preserveCurrentRoot;
            PlayPreviousEntityDisappearanceAnimation = playLeafDisappearanceAnimation;
        }
    }
}
