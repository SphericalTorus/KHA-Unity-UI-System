using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIPoolSimple : MonoBehaviour, IUIPool
    {
        [SerializeField] private List<UIEntity> _prototypes;

        private readonly Dictionary<Type, List<IUISystemEntity>> _activeEntities = new();
        private readonly Dictionary<Type, List<IUISystemEntity>> _inactiveEntities = new();
        private readonly Dictionary<Type, GameObject> _prototypesByType = new();

        public void Init()
        {
            foreach (var prototype in _prototypes)
            {
                var type = prototype.GetComponent<UIEntity>().GetType();

                if (!_prototypesByType.ContainsKey(type))
                {
                    _prototypesByType.Add(type, prototype.gameObject);
                    
                    if (prototype.Prewarm)
                    {
                        var prewarmedEntity = InstantiateUIEntity(prototype.gameObject);
                        var prewarmedEntityTransform = prewarmedEntity.transform;
                        prewarmedEntityTransform.SetParent(transform);
                        prewarmedEntityTransform.localPosition = Vector3.zero;
                        prewarmedEntityTransform.gameObject.SetActive(false);
                        var entityType = prewarmedEntity.GetType();

                        if (!_inactiveEntities.ContainsKey(entityType))
                        {
                            _inactiveEntities.Add(entityType, new List<IUISystemEntity>());
                        }

                        _inactiveEntities[entityType].Add(prewarmedEntity);
                    }
                }
                else
                {
                    UILogger.LogError($"Duplicated UIEntity in simple UI pool of type {type}.");
                }
            }
        }

        public T Get<T>() where T : MonoBehaviour, IUISystemEntity
        {
            var entityType = typeof(T);
            return Get(entityType) as T;
        }

        public IUISystemEntity Get(Type entityType)
        {
            IUISystemEntity inactiveEntity;

            if (_inactiveEntities.ContainsKey(entityType) &&
                _inactiveEntities[entityType].Count > 0)
            {
                inactiveEntity = _inactiveEntities[entityType][0];
                _inactiveEntities[entityType].RemoveAt(0);
            }
            else
            {
                inactiveEntity = InstantiateUIEntity(_prototypesByType[entityType]);
            }

            if (!_activeEntities.ContainsKey(entityType))
            {
                _activeEntities.Add(entityType, new List<IUISystemEntity>());
            }

            _activeEntities[entityType].Add(inactiveEntity);
            inactiveEntity.GameObject.SetActive(false);

            return inactiveEntity;
        }

        public void Release(IUISystemEntity entity)
        {
            var entityType = entity.GetType();

            if (_activeEntities.ContainsKey(entityType) && _activeEntities[entityType].Count > 0)
            {
                var activeEntity = _activeEntities[entityType]
                    .Find(e => e.GetHashCode() == entity.GetHashCode());

                if (activeEntity == null)
                {
                    LogReleaseError(entity);

                    return;
                }

                var activeEntityTransform = activeEntity.GameObject.transform;
                activeEntityTransform.SetParent(transform);
                activeEntityTransform.localPosition = Vector3.zero;
                activeEntity.GameObject.SetActive(false);
                _activeEntities[entityType].Remove(activeEntity);

                if (_activeEntities[entityType].Count == 0)
                {
                    _activeEntities.Remove(entityType);
                }

                if (!_inactiveEntities.ContainsKey(entityType))
                {
                    _inactiveEntities.Add(entityType, new List<IUISystemEntity>());
                }

                _inactiveEntities[entityType].Add(activeEntity);
            }
            else
            {
                LogReleaseError(entity);
            }
        }

        private UIEntity InstantiateUIEntity(GameObject prototype)
        {
            var entity = Instantiate(prototype);
            return entity.GetComponent<UIEntity>();
        }

        private void LogReleaseError(IUISystemEntity entity)
        {
            Debug.LogError(
                $"An attempt to release ui entity which is not under control of the pool: " +
                $"{entity.GameObject.name}");

            var entityType = entity.GetType();

            if (!_inactiveEntities.ContainsKey(entityType) || !_inactiveEntities[entityType].Contains(entity))
            {
                Destroy(entity.GameObject);   
            }
        }
    }
}
