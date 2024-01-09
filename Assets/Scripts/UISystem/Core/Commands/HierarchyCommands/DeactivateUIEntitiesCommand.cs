using System.Collections.Generic;

namespace Kha.UI.Core
{
    /// <summary>
    /// Command which either releases a set of UI entities, either hides them.
    /// </summary>
    public abstract class DeactivateEntitiesUICommand : UICommand
    {
        private readonly HierarchicalEntitiesStorage _hierarchy;
        private readonly IUIPool _uiPool;
        private readonly int _targetHierarchyDepth;
        private readonly UICallbacksBridge _uiCallbacksBridge;
        private bool _isCompleted;
        private readonly bool _playLeafAnimation;
        private IUISystemEntity _entityToPlayTransition;

        private readonly List<IUISystemEntity> _hierarchyCopy = new();

        protected DeactivateEntitiesUICommand(HierarchicalEntitiesStorage hierarchy, 
            IUIPool uiPool, bool playLeafHideAnimation, int depthToStop, UICallbacksBridge uiCallbacksBridge)
        {
            _hierarchy = hierarchy;
            _uiPool = uiPool;
            _playLeafAnimation = playLeafHideAnimation;
            _targetHierarchyDepth = depthToStop;
            _uiCallbacksBridge = uiCallbacksBridge;
        }

        public override void Execute()
        {   
            if (_hierarchy.GetDepth() <= _targetHierarchyDepth ||
                _hierarchy.GetDepth() == 0)
            {
                _isCompleted = true;
                return;
            }

            if (_playLeafAnimation)
            {
                _entityToPlayTransition = _hierarchy.GetLeaf();
                DeactivateWithTransition(_entityToPlayTransition);
            }
            else
            {
                DeactivateAllUIEntities();
            }
        }

        public override bool IsCompleted()
        {
            return _isCompleted;
        }

        abstract protected bool Release();

        private void DeactivateAllUIEntities()
        {
            _hierarchyCopy.Clear();
            
            foreach (var item in _hierarchy)
            {
                _hierarchyCopy.Add(item);
            }

            for (var i = _hierarchyCopy.Count - 1; i >= 0; i--)
            {
                if (i <= _targetHierarchyDepth - 1)
                {
                    break;
                }

                var uiSystemEntity = _hierarchyCopy[i];

                if (uiSystemEntity != _entityToPlayTransition)
                {
                    DeactivateInstantly(uiSystemEntity);
                }
            }

            _isCompleted = true;
        }

        private void DeactivateWithTransition(IUISystemEntity uiEntity)
        {
            uiEntity.Notify(UIEntityNotification.DisappearanceStarted, _uiCallbacksBridge);
            uiEntity.ResolveDisappearanceAnimation().Play(() =>
            {
                DeactivateEntity(uiEntity);
                uiEntity.Notify(UIEntityNotification.DisappearanceCompleted, _uiCallbacksBridge);
                DeactivateAllUIEntities();
            });            
        }

        private void DeactivateInstantly(IUISystemEntity uiEntity)
        {
            uiEntity.Notify(UIEntityNotification.DisappearanceStarted, _uiCallbacksBridge);
            DeactivateEntity(uiEntity);
            uiEntity.Notify(UIEntityNotification.DisappearanceCompleted, _uiCallbacksBridge);
        }

        private void DeactivateEntity(IUISystemEntity uiEntity)
        {
            uiEntity.GameObject.SetActive(false);

            if (Release())
            {
                _hierarchy.RemoveLeaf(uiEntity);
                _uiPool.Release(uiEntity);
                uiEntity.Notify(UIEntityNotification.Released, _uiCallbacksBridge);
            }
        }
    }
}
