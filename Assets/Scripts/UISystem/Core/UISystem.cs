using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Facade of the UI system.
    /// </summary>
    public sealed class UISystem : MonoBehaviour
    {
        [Tooltip("Root canvas.")]
        [SerializeField] private Canvas _mainCanvas;
        [Tooltip("Dedicated UI camera. Is used to render 3d objects in UI.")]
        [SerializeField] private Camera _uiCamera;
        [Tooltip("Holder for all world windows.")]
        [SerializeField] private UIEntitiesHolder _worldWindowsHolder;
        [Tooltip("Holder for all panels which must be displayed under windows.")]
        [SerializeField] private UIEntitiesHolder _underlyingPanelsHolder;
        [Tooltip("Holder for all windows.")]
        [SerializeField] private UIEntitiesHolder _windowsHolder;
        [Tooltip("Holder for all panels which must be displayed on to of windows.")]
        [SerializeField] private UIEntitiesHolder _overlayingPanelsHolder;
        [Tooltip("OnGUI overlay for debug.")]
        [SerializeField] private OnGUIOverlay _onGuiOverlay;
        [Tooltip("Pool with ui entities.")]
        [SerializeField] private UIPoolSimple _uiPool;
        
        private readonly UICallbacksBridge _uiCallbacksBridge = new();
        private bool _isInitialized;
        private WindowsSystem _windowsSystem;
        private PanelsSystem _panelsSystem;
        private WorldWindowsSystem _worldWindowsSystem;

        /// <summary>
        /// Part of the UI system responsible for windows logic.
        /// </summary>
        public IWindowsSystem Windows => _windowsSystem;
        
        /// <summary>
        /// Part of the UI system responsible for panels logic.
        /// </summary>
        public IPanelsSystem Panels => _panelsSystem;
        
        /// <summary>
        /// Part of the UI system responsible for world windows.
        /// </summary>
        public IWorldWindowsSystem WorldWindows => _worldWindowsSystem;
        
        /// <summary>
        /// Holder of all UI events.
        /// </summary>
        public IUICallbacksBridge Events => _uiCallbacksBridge;

        private void Awake()
        {
            _windowsSystem = new WindowsSystem(_windowsHolder, _uiPool, _uiCallbacksBridge);
            _panelsSystem = new PanelsSystem(_underlyingPanelsHolder, _overlayingPanelsHolder, _uiPool, _uiCallbacksBridge);
            _worldWindowsSystem = new WorldWindowsSystem(_worldWindowsHolder, _uiPool, _uiCallbacksBridge);

            _uiPool.Init();
            _isInitialized = true;
            SwitchToOverlayMode();

            _uiCallbacksBridge.UIEntityAppearanceStarted += OnUIEntityAppearanceStarted;
            _uiCallbacksBridge.UIEntityDisappearanceFinished += OnUIEntityDisappearanceFinished;
        }

        private void OnDestroy()
        {
            _uiCallbacksBridge.UIEntityAppearanceStarted -= OnUIEntityAppearanceStarted;
            _uiCallbacksBridge.UIEntityDisappearanceFinished -= OnUIEntityDisappearanceFinished;
        }

        private void Update()
        {
            if (_isInitialized)
            {
                _windowsSystem.Controller.Update();
                _worldWindowsSystem.Controller.Update();
            }
        }

        /// <summary>
        /// Main initialization method.
        /// </summary>
        /// <param name="worldCamera">Camera which renders gameplay.</param>
        public void Initialize(Camera worldCamera)
        {
            _worldWindowsSystem.Controller.InjectWorldCamera(worldCamera);
        }
        
        /// <summary>
        /// In case of a change of the main world camera, UI system must be notified via this method.
        /// </summary>
        /// <param name="worldCamera">New main world camera.</param>
        public void UpdateWorldCamera(Camera worldCamera)
        {
            _worldWindowsSystem.Controller.InjectWorldCamera(worldCamera);
        }

        /// <summary>
        /// Activate or deactivates script, responsible for the overlay drawn with OnGUI method.
        /// </summary>
        /// <param name="activate">Activate or deactivate.</param>
        /// <returns>OnGUI overlay controller.</returns>
        public OnGUIOverlay ActivateOnGUIOverlay(bool activate)
        {
            _onGuiOverlay.gameObject.SetActive(activate);
            return _onGuiOverlay;
        }
        
        private void OnUIEntityAppearanceStarted(IUISystemEntity uiEntity)
        {
            if (uiEntity.Has3dObject)
            {
                SwitchToCameraMode();
            }
        }
        
        private void OnUIEntityDisappearanceFinished(IUISystemEntity uiEntity)
        {
            if (uiEntity.Has3dObject)
            {
                SwitchToOverlayMode();
            }
        }
        
        private void SwitchToCameraMode()
        {
            _uiCamera.gameObject.SetActive(true);
            _mainCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            _mainCanvas.worldCamera = _uiCamera;
            _worldWindowsSystem.Controller.InjectUICamera(GetActiveUICamera());
        }
        
        private void SwitchToOverlayMode()
        {
            _uiCamera.gameObject.SetActive(false);
            _mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _worldWindowsSystem.Controller.InjectUICamera(GetActiveUICamera());
        }

        private Camera GetActiveUICamera()
        {
            return _uiCamera.gameObject.activeSelf ? _uiCamera : null;
        }
    }
}
