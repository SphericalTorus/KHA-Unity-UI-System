using System;
using System.Collections;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIAnimationAppearFromTop : UIAnimationBase
    {
        [SerializeField] private RectTransform _root;
        [SerializeField] private float _duration;

        private Vector2 _originalAnchoredPosition;
        private Coroutine _animationCoroutine;

        private void Awake()
        {
            _originalAnchoredPosition = _root.anchoredPosition;
        }

        protected override void PlayAnimation(Action onComplete)
        {
            if (_animationCoroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = CoroutineStarter.Instance.StartCoroutine(Animate(_originalAnchoredPosition.y, onComplete));
        }

        protected override void InterruptAnimation()
        {
            if (_animationCoroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(_animationCoroutine);
                _animationCoroutine = null;
            }
        }

        private IEnumerator Animate(float targetY, Action onComplete)
        {
            _root.anchoredPosition = new Vector2(_root.anchoredPosition.x, _root.rect.height);
            var elapsedTime = 0f;
            var startY = _root.anchoredPosition.y;

            while (elapsedTime < _duration)
            {
                var newY = Mathf.Lerp(startY, targetY, elapsedTime / _duration);
                _root.anchoredPosition = new Vector2(_root.anchoredPosition.x, newY);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _root.anchoredPosition = new Vector2(_root.anchoredPosition.x, targetY);
            onComplete?.Invoke();
        }
    }
}
