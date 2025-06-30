using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIOwner : MonoBehaviour
    {
        [SerializeField] private List<Renderer> _renderers;
        [SerializeField] private Vector3 _uiOffset;

        public Vector3 UIOffset => _uiOffset;

        private bool _isRenderersListInvalid;

        private void Awake()
        {
            if (_renderers.Count == 0)
            {
                UILogger.LogError($"Renderers are not linked to UIOwner component of {gameObject.name} object. " +
                    $"UI owner will be considered visible constantly.");
                _isRenderersListInvalid = true;
            }

            foreach (var rendererComponent in _renderers)
            {
                if (rendererComponent == null)
                {
                    UILogger.LogError($"UIOwner component of {gameObject.name} object has missing references in renderers list. " +
                        $"UI owner will be considered visible constantly.");
                    _isRenderersListInvalid = true;
                }
            }
        }

        public bool IsVisible()
        {
            return _isRenderersListInvalid || _renderers.Any(r => r.isVisible);
        }
    }
}
