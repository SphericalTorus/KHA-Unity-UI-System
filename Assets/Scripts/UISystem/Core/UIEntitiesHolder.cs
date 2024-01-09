using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIEntitiesHolder : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;
    }
}
