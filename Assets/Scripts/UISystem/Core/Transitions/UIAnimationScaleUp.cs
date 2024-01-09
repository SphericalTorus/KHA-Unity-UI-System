using DG.Tweening;
using System;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIAnimationScaleUp : UIAnimationBase
    {
        [SerializeField] private float _duration = 0.35f;
        [SerializeField] private float _startingScale;
        [SerializeField] private RectTransform _objectToScale;

        private Tween _tween;

        protected override void PlayAnimation(Action onComplete)
        {
            _tween?.Kill();
            _objectToScale.localScale = Vector3.one * _startingScale;
            _tween = _objectToScale
                .DOScale(Vector3.one, _duration)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
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
