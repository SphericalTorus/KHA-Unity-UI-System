namespace Kha.UI.Core
{
    public struct FlatShowStrategy : IFlatShowStrategy
    {
        public bool IsFullScreen { get; }
        public bool IsOverlaying { get; }

        public static IFlatShowStrategy Default => new FlatShowStrategy(true, true);

        private FlatShowStrategy(bool fullScreen, bool isOverlaying)
        {
            IsFullScreen = fullScreen;
            IsOverlaying = isOverlaying;
        }
    }
}