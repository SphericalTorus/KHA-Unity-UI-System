using System.Collections.Generic;
using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Logic which manipulates UI entities of a hierarchical layer.
    /// Creates, holds and executes related commands.
    /// </summary>
    public sealed class HierarchicalLayerController
    {
        private readonly IUIPool _uiPool;
        private readonly UICallbacksBridge _uiCallbacksBridge;
        private readonly Queue<UICommand> _commandsQueue = new();
        private UICommand _currentUICommand;

        public bool HasScheduledCommands => _commandsQueue.Count > 0;

        public HierarchicalLayerController(IUIPool uiPool, UICallbacksBridge uiCallbacksBridge)
        {
            _uiPool = uiPool;
            _uiCallbacksBridge = uiCallbacksBridge;
        }

        public void Update()
        {
            TryCompleteCurrentCommand();

            while (_commandsQueue.Count > 0 && _currentUICommand == null)
            {
                _currentUICommand = _commandsQueue.Dequeue();
                _currentUICommand.Execute();

                TryCompleteCurrentCommand();
            }
        }

        public IShowUIEntityCommand<T> ShowEntity<T>(HierarchicalLayer layer, IShowInHierarchyStrategy showStrategy)
            where T : MonoBehaviour, IUISystemEntity
        {
            if (showStrategy.ActionOnOthers == ActionOnOthers.ReleaseAll)
            {
                var depthToStopDeactivating = showStrategy.PreserveCurrentRoot ? 1 : 0;
                _commandsQueue.Enqueue(new ReleaseUIEntitiesCommand(
                    layer.Hierarchy, _uiPool, showStrategy.PlayPreviousEntityDisappearanceAnimation, depthToStopDeactivating,
                    _uiCallbacksBridge));
            }
            else if (showStrategy.ActionOnOthers == ActionOnOthers.HideAll)
            {
                var depthToStopDeactivating = showStrategy.PreserveCurrentRoot ? 1 : 0;
                _commandsQueue.Enqueue(new HideUIEntitiesCommand(
                    layer.Hierarchy, _uiPool, showStrategy.PlayPreviousEntityDisappearanceAnimation, depthToStopDeactivating,
                    _uiCallbacksBridge));
            }

            var showUIEntityCommand = new ShowUIEntityCommand<T>(_uiPool, layer, _uiCallbacksBridge);
            
            _commandsQueue.Enqueue(showUIEntityCommand);

            return showUIEntityCommand;
        }

        public void HideEntity<T>(HierarchicalLayer layer, IHideInHierarchyStrategy hideStrategy) where T : MonoBehaviour, IUISystemEntity
        {
            var entityToRelease = layer.Hierarchy.FindNode<T>(out var entityToReleaseDepth);
            HideEntityInternal(entityToRelease, layer, hideStrategy, entityToReleaseDepth);
        }
        
        public void HideEntity(IUISystemEntity uiEntity, HierarchicalLayer layer, IHideInHierarchyStrategy hideStrategy)
        {
            var entityToRelease = layer.Hierarchy.FindNode(uiEntity);
            HideEntityInternal(entityToRelease, layer, hideStrategy, 0);
        }

        private void HideEntityInternal(IUISystemEntity entityToRelease, HierarchicalLayer layer, 
            IHideInHierarchyStrategy hideStrategy, int entityToReleaseDepth)
        {
            if (entityToRelease == null)
            {
                return;
            }

            _commandsQueue.Enqueue(new ReleaseUIEntitiesCommand(layer.Hierarchy, _uiPool, true, 
                entityToReleaseDepth, _uiCallbacksBridge));

            if (hideStrategy.ShowOthers)
            {
                _commandsQueue.Enqueue(new ActivateAllUIEntitiesCommand(layer.Hierarchy, 
                    hideStrategy.PlayAppearanceAnimationForPreviousEntity));
            }
            else 
            {
                var previousEntity = layer.Hierarchy.TryGetWithDepth(entityToReleaseDepth - 1);

                if (previousEntity != null && !previousEntity.IsShown)
                {
                    _commandsQueue.Enqueue(new ActivateUIEntityCommand(previousEntity, 
                        hideStrategy.PlayAppearanceAnimationForPreviousEntity, _uiCallbacksBridge));
                }
            }
        }

        private void TryCompleteCurrentCommand()
        {
            if (_currentUICommand != null && _currentUICommand.IsCompleted())
            {
                _currentUICommand = null;
            }
        }
    }
}
