using DG.Tweening;
using System;
using UnityEngine;

namespace Kha.UI.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class UIAnimationFadeIn : UIAnimationBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 0.1f;

        private Tween _tween;

        protected override void PlayAnimation(Action onComplete)
        {
            _canvasGroup.alpha = 0f;
            _tween?.Kill();
            _tween = _canvasGroup
                .DOFade(1f, _duration)
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
