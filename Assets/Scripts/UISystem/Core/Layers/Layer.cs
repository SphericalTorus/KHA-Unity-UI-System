using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Base class for all UI layers.
    /// </summary>
    public abstract class Layer
    {
        private readonly UIEntitiesHolder _entitiesHolder;

        public Transform EntitiesHolder => _entitiesHolder.transform;
        
        protected Layer(UIEntitiesHolder entitiesHolder)
        {
            _entitiesHolder = entitiesHolder;
        }
    }
}
