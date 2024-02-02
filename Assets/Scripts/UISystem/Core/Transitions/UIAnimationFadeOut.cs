using System;
using System.Collections;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIAnimationFadeOut : UIAnimationBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 0.1f;

        private Coroutine _animationCoroutine;

        private void OnDisable()
        {
            _canvasGroup.alpha = 1f;
        }

        protected override void PlayAnimation(Action onComplete)
        {
            if (_animationCoroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = CoroutineStarter.Instance.StartCoroutine(FadeOut(onComplete));
        }

        protected override void InterruptAnimation()
        {
            if (_animationCoroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(_animationCoroutine);
                _animationCoroutine = null;
            }
        }

        private IEnumerator FadeOut(Action onComplete)
        {
            var elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                var newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / _duration);
                _canvasGroup.alpha = newAlpha;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _canvasGroup.alpha = 0f;
            onComplete?.Invoke();
        }
    }
}