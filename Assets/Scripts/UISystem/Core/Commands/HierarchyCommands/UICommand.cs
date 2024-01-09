namespace Kha.UI.Core
{
    /// <summary>
    /// Base class for any UI command.
    /// </summary>
    public abstract class UICommand
    {
        public abstract void Execute();
        public abstract bool IsCompleted();
    }
}
