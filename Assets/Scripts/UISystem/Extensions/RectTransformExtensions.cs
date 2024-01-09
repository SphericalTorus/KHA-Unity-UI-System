using UnityEngine;

namespace Kha.UI.Extensions
{
    /// <summary>
    /// Extensions related to RectTransform.
    /// </summary>
    public static class RectTransformExtensions
    {
        public static Vector3 GetWorldCenterPosition(this RectTransform rectTransform)
        {
            var rectWorldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(rectWorldCorners);

            return rectWorldCorners[0] + 0.5f * (rectWorldCorners[2] - rectWorldCorners[0]);
        }

        public static void Set(this RectTransform rectTransform, RectTransform copyFrom)
        {
            rectTransform.pivot = copyFrom.pivot;
            rectTransform.anchorMin = copyFrom.anchorMin;
            rectTransform.anchorMax = copyFrom.anchorMax;
            rectTransform.anchoredPosition = copyFrom.anchoredPosition;
            rectTransform.sizeDelta = copyFrom.sizeDelta;
            rectTransform.position = copyFrom.position;
        }
        
        public static void MakeFullScreen(this RectTransform rectTransform)
        {
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}