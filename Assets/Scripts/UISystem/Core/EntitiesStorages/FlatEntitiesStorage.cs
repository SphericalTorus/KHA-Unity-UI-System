using System;
using System.Collections;
using System.Collections.Generic;

namespace Kha.UI.Core
{
    /// <summary>
    /// A bunch of independent entities.
    /// </summary>
    public sealed class FlatEntitiesStorage : IEnumerable<IUISystemEntity>
    {
        private readonly Dictionary<Type, List<IUISystemEntity>> _entitiesByType = new();

        private readonly static List<IUISystemEntity> EmptyList = new();

        public void Add(IUISystemEntity uiEntity)
        {
            var type = uiEntity.GetType();

            if (!_entitiesByType.ContainsKey(type))
            {
                _entitiesByType.Add(type, new List<IUISystemEntity>());
            }

            _entitiesByType[type].Add(uiEntity);
        }

        public IReadOnlyList<IUISystemEntity> Get<T>() where T : IUISystemEntity
        {
            var type = typeof(T);
            return _entitiesByType.GetValueOrDefault(type, EmptyList);
        }

        public void Remove(IUISystemEntity uiEntity)
        {
            var type = uiEntity.GetType();

            if (_entitiesByType.TryGetValue(type, out var entities))
            {
                entities.Remove(uiEntity);
            }
        }

        public bool HasEntity<T>() where T : IUISystemEntity
        {
            var type = typeof(T);
            return _entitiesByType.ContainsKey(type) && _entitiesByType[type].Count > 0;
        }

        public IEnumerator<IUISystemEntity> GetEnumerator()
        {
            return GetAllEntities();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetAllEntities();
        }

        private IEnumerator<IUISystemEntity> GetAllEntities()
        {
            foreach (var entitiesOfType in _entitiesByType.Values)
            {
                foreach (var entity in entitiesOfType)
                {
                    yield return entity;
                }
            }
        }
    }
}
