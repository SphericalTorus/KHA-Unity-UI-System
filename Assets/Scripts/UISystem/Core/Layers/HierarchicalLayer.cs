namespace Kha.UI.Core
{
    /// A holder of UI entities, which are constructing a hierarchy.
    public sealed class HierarchicalLayer : Layer
    {
        public HierarchicalEntitiesStorage Hierarchy { get; } = new();

        public HierarchicalLayer(UIEntitiesHolder canvas) : base(canvas)
        {
        }
    }
}
