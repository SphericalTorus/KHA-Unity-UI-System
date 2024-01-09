using DG.Tweening;
using System;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIAnimationAppearFromTop : UIAnimationBase
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
            _tween?.Kill();
            _root.anchoredPosition = new Vector2(_root.anchoredPosition.x, _root.rect.height);
            _tween = _root
                .DOAnchorPosY(_originalAnchoredPosition.y, _duration)
                .SetEase(Ease.InOutBack)
                .OnComplete(() => onComplete?.Invoke());
        }
        
        protected override void InterruptAnimation()
        {
            _tween?.Kill();
            _tween = null;
        }
    }
}
