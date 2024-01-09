using System;

namespace Kha.UI.Core
{
    public sealed class UIAnimationNone : UIAnimationBase
    {
        protected override void PlayAnimation(Action onComplete)
        {
            // Do nothing
            onComplete?.Invoke();
        }

        protected override void InterruptAnimation()
        {
        }
    }
}
