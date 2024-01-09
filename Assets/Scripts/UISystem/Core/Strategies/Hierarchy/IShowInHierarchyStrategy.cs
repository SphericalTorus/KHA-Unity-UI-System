namespace Kha.UI.Core
{
    public interface IShowInHierarchyStrategy
    {
        ActionOnOthers ActionOnOthers { get; }
        bool PreserveCurrentRoot { get; }
        bool PlayPreviousEntityDisappearanceAnimation { get; }
    }
}
