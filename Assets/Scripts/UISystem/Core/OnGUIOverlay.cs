using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class OnGUIOverlay : MonoBehaviour
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR

        private GUIStyle _guiStyle;
        
        private void Awake()
        {
            gameObject.SetActive(false);
            CreateGUIStyles();
        }

        private void OnGUI()
        {
            DrawExampleLabel();
        }

        private void CreateGUIStyles()
        {
            var backgroundTexture = new Texture2D(1, 1);
            backgroundTexture.SetPixel(0, 0, new Color(0.4f, 0.34f, 0.4f, 0.7f));
            backgroundTexture.Apply();

            _guiStyle = new GUIStyle
            {
                fontSize = 30,
                normal =
                {
                    textColor = Color.white,
                    background = backgroundTexture,
                },
                alignment = TextAnchor.MiddleCenter,
            };
        }

        private void DrawExampleLabel()
        {
            const float width = 600f;
            const float height = 40f;
            var x = 0.5f * Screen.width - 0.5f * width;
            const float y = height;
            GUI.Label(new Rect(x, y, width, height), "Some debug information could be here.", _guiStyle);
        }
#endif
    }
}
