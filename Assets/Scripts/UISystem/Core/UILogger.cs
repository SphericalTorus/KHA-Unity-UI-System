using System.Diagnostics;

namespace Kha.UI.Core
{
    public static class UILogger
    {
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(object text)
        {
                UnityEngine.Debug.Log(text);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(object text)
        {
            UnityEngine.Debug.LogWarning(text);
        }

        public static void LogError(object text)
        {
            UnityEngine.Debug.LogError(text);
        }
    }
}
