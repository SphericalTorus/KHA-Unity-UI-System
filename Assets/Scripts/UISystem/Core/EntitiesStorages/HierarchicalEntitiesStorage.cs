using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Just a list which imitates stack. One day it might become a tree...
    /// </summary>
    public sealed class HierarchicalEntitiesStorage : IEnumerable
    {
        private readonly List<IUISystemEntity> _entities = new();

        public IUISystemEntity GetLeaf()
        {
            return _entities.LastOrDefault();
        }

        public IUISystemEntity TryGetWithDepth(int depth)
        {
            if (_entities.Count > depth && depth >= 0)
            {
                return _entities[depth];
            }

            return null;
        }

        public int GetDepth()
        {
            return _entities.Count;
        }

        public void RemoveLeaf(IUISystemEntity uiEntity)
        {
            if (_entities.Count > 0 && _entities.Last() == uiEntity)
            {
                _entities.Remove(uiEntity);
            }
        }

        public void AddLeaf(IUISystemEntity uiEntity)
        {
            _entities.Add(uiEntity);
        }

        public T FindNode<T>(out int depth) where T : MonoBehaviour, IUISystemEntity
        {
            for (var i = _entities.Count - 1; i >= 0; i--)
            {
                if (_entities[i] is T entity)
                {
                    depth = i;
                    return entity;
                }
            }

            depth = -1;
            return null;
        }
        
        public IUISystemEntity FindNode(IUISystemEntity uiEntity) 
        {
            return _entities.Find(e => e == uiEntity);
        }

        public bool HasNode<T>() where T : MonoBehaviour, IUISystemEntity
        {
            return FindNode<T>(out var depth) != null;
        }

        public IEnumerator<IUISystemEntity> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _entities.GetEnumerator();
        }
    }
}
