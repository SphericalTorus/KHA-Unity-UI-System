using DG.Tweening;
using System;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIAnimationDisappearToTop : UIAnimationBase
    {
        [SerializeField] private RectTransform _root;
        [SerializeField] private float _duration;

        private Vector2 _originalAnchoredPosition;
        private Tween _tween;

        private void Awake()
        {
            _originalAnchoredPosition = _root.anchoredPosition;
        }

        protected override void PlayAnimation(Action onComplete)
        {
            _tween = _root
                .DOAnchorPosY(_root.rect.height, _duration)
                .SetEase(Ease.InOutBack)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                    _root.anchoredPosition = _originalAnchoredPosition;
                    _tween = null;
                });
        }

        protected override void InterruptAnimation()
        {
            _tween?.Kill();
            _tween = null;
        }
    }
}
