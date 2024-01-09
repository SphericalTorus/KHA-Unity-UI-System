namespace Kha.UI.Core
{
    /// <summary>
    /// A holder of UI entities, which are independent from each other.
    /// </summary>
    public sealed class FlatLayer : Layer
    {
        public FlatEntitiesStorage Entities { get; } = new();

        public FlatLayer(UIEntitiesHolder entitiesHolder) : base(entitiesHolder)
        {
        }
    }
}
