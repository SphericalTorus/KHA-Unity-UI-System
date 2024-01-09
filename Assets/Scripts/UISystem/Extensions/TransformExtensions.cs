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
        
        public static bool IsOnScreen(this Transform transform, Camera camera)
        {
            var frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
            var newBounds = new Bounds(transform.position, Vector3.one * 0.02f);
            return GeometryUtility.TestPlanesAABB(frustumPlanes, newBounds);
        }
    }
}


