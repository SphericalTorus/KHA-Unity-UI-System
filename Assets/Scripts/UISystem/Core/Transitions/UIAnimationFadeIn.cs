using System;
using System.Collections;
using UnityEngine;

namespace Kha.UI.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class UIAnimationFadeIn : UIAnimationBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 0.1f;

        private Coroutine _animationCoroutine;

        protected override void PlayAnimation(Action onComplete)
        {
            if (_animationCoroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = CoroutineStarter.Instance.StartCoroutine(FadeIn(onComplete));
        }

        protected override void InterruptAnimation()
        {
            if (_animationCoroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(_animationCoroutine);
                _animationCoroutine = null;
            }
        }

        private IEnumerator FadeIn(Action onComplete)
        {
            _canvasGroup.alpha = 0f;
            var elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                var newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / _duration);
                _canvasGroup.alpha = newAlpha;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _canvasGroup.alpha = 1f;
            onComplete?.Invoke();
        }
    }
}