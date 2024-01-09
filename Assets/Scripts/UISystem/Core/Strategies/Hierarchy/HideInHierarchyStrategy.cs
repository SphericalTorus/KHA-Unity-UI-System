namespace Kha.UI.Core
{
    public struct HideInHierarchyStrategy : IHideInHierarchyStrategy
    {
        public bool ShowOthers { get; }
        public bool PlayAppearanceAnimationForPreviousEntity { get; }

        public HideInHierarchyStrategy(bool showOthers, bool playAppearanceAnimationForNewLeaf)
        {
            ShowOthers = showOthers;
            PlayAppearanceAnimationForPreviousEntity = playAppearanceAnimationForNewLeaf;
        }

        public static IHideInHierarchyStrategy DefaultCut => new HideInHierarchyStrategy(false, false);
        public static IHideInHierarchyStrategy Default => new HideInHierarchyStrategy(false, true);
        public static IHideInHierarchyStrategy ActivateAllPreviousCut => new HideInHierarchyStrategy(true, false);
        public static IHideInHierarchyStrategy ActivateAllPrevious => new HideInHierarchyStrategy(true, true);
    }
}
