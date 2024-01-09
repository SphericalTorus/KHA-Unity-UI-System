using DG.Tweening;
using System;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIAnimationScaleDown : UIAnimationBase
    {
        [SerializeField] private float _duration = 0.35f;
        [SerializeField] private float _targetScale;
        [SerializeField] private RectTransform _objectToScale;

        private Tween _tween;
        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = _objectToScale.localScale;
        }

        protected override void PlayAnimation(Action onComplete)
        {
            _tween?.Kill();
            _tween = _objectToScale
                .DOScale(_targetScale, _duration)
                .SetEase(Ease.OutCirc)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                    _objectToScale.localScale = _originalScale;
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
