using System;
using System.Collections;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIAnimationScaleUp : UIAnimationBase
    {
        [SerializeField] private float _duration = 0.35f;
        [SerializeField] private float _startingScale;
        [SerializeField] private RectTransform _objectToScale;

        private Coroutine _animationCoroutine;

        protected override void PlayAnimation(Action onComplete)
        {
            if (_animationCoroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = CoroutineStarter.Instance.StartCoroutine(ScaleUp(onComplete));
        }

        protected override void InterruptAnimation()
        {
            if (_animationCoroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(_animationCoroutine);
                _animationCoroutine = null;
            }
        }

        private IEnumerator ScaleUp(Action onComplete)
        {
            var elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                var newScale = Mathf.Lerp(_startingScale, 1f, elapsedTime / _duration);
                _objectToScale.localScale = Vector3.one * newScale;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _objectToScale.localScale = Vector3.one;
            onComplete?.Invoke();
        }
    }
}