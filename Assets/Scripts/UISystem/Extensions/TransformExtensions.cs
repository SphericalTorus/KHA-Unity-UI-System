using UnityEngine;

namespace Kha.UI.Extensions
{
    /// <summary>
    /// Extensions related to Transform.
    /// </summary>
    public static class TransformExtensions
    {
        public static void TryMakeFullScreen(this Transform transform)
        {
            var rectTransform = transform.GetComponent<RectTransform>();

            if (rectTransform == null)
            {
                return;
            }

            rectTransform.MakeFullScreen();
        }
    }
}


