namespace Kha.UI.Core
{
    /// <summary>
    /// Command which activates all UI entities in a hierarchy.
    /// </summary>
    public sealed class ActivateAllUIEntitiesCommand : UICommand
    {
        private readonly HierarchicalEntitiesStorage _uiHierarchy;
        private readonly bool _playAppearanceAnimationForLeaf;
        private bool _isCompleted;

        public ActivateAllUIEntitiesCommand(HierarchicalEntitiesStorage uiHierarchy, bool playAppearanceAnimationForLeaf)
        {
            _uiHierarchy = uiHierarchy;
            _playAppearanceAnimationForLeaf = playAppearanceAnimationForLeaf;
        }

        public override void Execute()
        {
            foreach (var uiSystemEntity in _uiHierarchy)
            {
                uiSystemEntity.GameObject.SetActive(true);
            }

            if (_playAppearanceAnimationForLeaf && _uiHierarchy.GetDepth() > 0)
            {
                _uiHierarchy.GetLeaf().ResolveAppearanceAnimation().Play(() => _isCompleted = true);
            }
            else
            {
                _isCompleted = true;
            }
        }

        public override bool IsCompleted()
        {
            return _isCompleted;
        }
    }
}
