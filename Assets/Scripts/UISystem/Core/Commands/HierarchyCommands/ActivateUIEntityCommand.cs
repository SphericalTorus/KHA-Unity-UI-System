namespace Kha.UI.Core
{
    /// <summary>
    /// Command which activates an UI entity.
    /// </summary>
    public sealed class ActivateUIEntityCommand : UICommand
    {
        private readonly IUISystemEntity _uiEntity;
        private readonly bool _playAppearanceAnimation;
        private readonly UICallbacksBridge _uiCallbacksBridge;
        private bool _isCompleted;

        public ActivateUIEntityCommand(IUISystemEntity uiEntity, bool playAppearanceAnimation, UICallbacksBridge uiCallbacksBridge)
        {
            _uiEntity = uiEntity;
            _playAppearanceAnimation = playAppearanceAnimation;
            _uiCallbacksBridge = uiCallbacksBridge;
        }

        public override void Execute()
        {
            if (!_uiEntity.IsShown)
            {
                _uiEntity.Notify(UIEntityNotification.AppearanceStarted, _uiCallbacksBridge);
            }

            if (_playAppearanceAnimation)
            {
                _uiEntity.ResolveAppearanceAnimation().Play(CompleteActivation);
            }
            else
            {
                CompleteActivation();
            }
            
            _uiEntity.GameObject.SetActive(true);
        }

        public override bool IsCompleted()
        {
            return _isCompleted;
        }

        private void CompleteActivation()
        {
            _uiEntity.Notify(UIEntityNotification.AppearanceFinished, _uiCallbacksBridge);
            _isCompleted = true;
        }
    }
}
