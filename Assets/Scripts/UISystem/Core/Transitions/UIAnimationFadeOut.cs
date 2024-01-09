using DG.Tweening;
using System;
using UnityEngine;

namespace Kha.UI.Core
{
    public sealed class UIAnimationFadeOut : UIAnimationBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 0.1f;

        private Tween _tween;

        private void OnDisable()
        {
            _canvasGroup.alpha = 1f;
        }
        
        protected override void PlayAnimation(Action onComplete)
        {
            _tween?.Kill();
            _tween = _canvasGroup
                .DOFade(0f, _duration)
                .OnComplete(() => 
                {
                    _tween = null;
                    onComplete?.Invoke();                    
                });
        }

        protected override void InterruptAnimation()
        {
            _tween?.Kill();
            _tween = null;
        }
    }
}
