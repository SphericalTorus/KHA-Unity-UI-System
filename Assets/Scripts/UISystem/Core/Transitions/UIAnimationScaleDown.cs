using System;
using System.Collections;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIAnimationScaleDown : UIAnimationBase
    {
        [SerializeField] private float _duration = 0.35f;
        [SerializeField] private float _targetScale;
        [SerializeField] private RectTransform _objectToScale;

        private Coroutine _animationCoroutine;

        protected override void PlayAnimation(Action onComplete)
        {
            if (_animationCoroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = CoroutineStarter.Instance.StartCoroutine(ScaleDown(onComplete));
        }

        protected override void InterruptAnimation()
        {
            if (_animationCoroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(_animationCoroutine);
                _animationCoroutine = null;
            }
        }

        private IEnumerator ScaleDown(Action onComplete)
        {
            var elapsedTime = 0f;
            var startScale = _objectToScale.localScale;

            while (elapsedTime < _duration)
            {
                var newScale = Mathf.Lerp(startScale.x, _targetScale, elapsedTime / _duration);
                _objectToScale.localScale = new Vector3(newScale, newScale, newScale);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _objectToScale.localScale = new Vector3(_targetScale, _targetScale, _targetScale);
            onComplete?.Invoke();
        }
    }
}