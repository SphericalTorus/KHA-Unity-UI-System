using System.Collections.Generic;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIOwner : MonoBehaviour
    {
        [SerializeField] private List<Renderer> _renderers;
        [SerializeField] private Vector3 _uiOffset;

        public Vector3 UIOffset => _uiOffset;

        private bool _isRenderersListCorrupted;

        private void Awake()
        {
            if (_renderers.Count == 0)
            {
                UILogger.LogError($"Renderers are not linked to UIOwner component of {gameObject.name} object. " +
                    $"UI owner will be considered visible constantly.");
                _isRenderersListCorrupted = true;
            }

            for (var i = 0; i < _renderers.Count; i++)
            {
                if (_renderers[i] == null)
                {
                    UILogger.LogError($"UIOwner component of {gameObject.name} object has missing references in renderers list. " +
                        $"UI owner will be considered visible constantly.");
                    _isRenderersListCorrupted = true;
                }
            }
        }

        public bool IsVisible()
        {
            if (_isRenderersListCorrupted)
            {
                return true;
            }
            
            for (var i = 0; i < _renderers.Count; i++)
            {
                if (_renderers[i].isVisible)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
