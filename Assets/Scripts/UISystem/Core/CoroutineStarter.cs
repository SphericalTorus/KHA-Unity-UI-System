using System.Collections;
using UnityEngine;

namespace Kha.UI.Core
{
    public class CoroutineStarter : MonoBehaviour
    {
        private static CoroutineStarter _instance;

        // Singleton pattern to ensure only one instance of CoroutineStarter exists
        public static CoroutineStarter Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("CoroutineStarter");
                    _instance = go.AddComponent<CoroutineStarter>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        private void OnDestroy()
        {
            _instance = null;
        }

        public new Coroutine StartCoroutine(IEnumerator routine)
        {
            return base.StartCoroutine(routine);
        }

        public new void StopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                base.StopCoroutine(coroutine);
            }
        }
    }
}