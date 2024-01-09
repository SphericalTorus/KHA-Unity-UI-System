using System;
using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Base class for animated transitions between UI entities.
    /// </summary>
    public abstract class UIAnimationBase : MonoBehaviour
    {
        private Action _finishCallback;

        public void Play(Action finishCallback = null)
        {
            _finishCallback = finishCallback;
            PlayAnimation(_finishCallback);
        }

        public void Interrupt()
        {
            InterruptAnimation();
            _finishCallback?.Invoke();
        }
        
        abstract protected void PlayAnimation(Action onComplete);
        abstract protected void InterruptAnimation();
    }
}
