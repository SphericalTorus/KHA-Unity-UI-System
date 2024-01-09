namespace Kha.UI.Core
{
    public interface IHideInHierarchyStrategy
    {
        bool ShowOthers { get; }
        bool PlayAppearanceAnimationForPreviousEntity { get; }
    }
}
