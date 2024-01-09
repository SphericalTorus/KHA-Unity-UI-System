namespace Kha.UI.Core
{
    public interface IFlatShowStrategy
    {
        bool IsFullScreen { get; }
        bool IsOverlaying { get; }
    }
}
