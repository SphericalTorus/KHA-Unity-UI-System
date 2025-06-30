using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Outer interface of the UI system part, which is responsible for windows logic.
    /// </summary>
    public interface IWindowsSystem
    {
        /// <summary>
        /// Shows a window with the specified strategy.
        /// </summary>
        /// <param name="showStrategy">How a window must be shown.</param>
        /// <typeparam name="T">Type of showing window.</typeparam>
        /// <returns>Command interface to specify additional options.</returns>
        IShowUIEntityCommand<T> Show<T>(IShowInHierarchyStrategy showStrategy) 
            where T : MonoBehaviour, IUISystemEntity;
        
        /// <summary>
        /// Shows a window with the default strategy.
        /// </summary>
        /// <typeparam name="T">Type of showing window.</typeparam>
        /// <returns>Command interface to specify additional options.</returns>
        IShowUIEntityCommand<T> Show<T>() 
            where T : MonoBehaviour, IUISystemEntity;
        
        /// <summary>
        /// Hides a window with the specified strategy.
        /// </summary>
        /// <param name="hideStrategy">How a window must be hidden.</param>
        /// <typeparam name="T">Type of hiding window.</typeparam>
        void Hide<T>(IHideInHierarchyStrategy hideStrategy) 
            where T : MonoBehaviour, IUISystemEntity;
        
        /// <summary>
        /// Hides a window with the default strategy.
        /// </summary>
        /// <typeparam name="T">Type of hiding window.</typeparam>
        void Hide<T>() 
            where T : MonoBehaviour, IUISystemEntity;

        /// <summary>
        /// Hides a window with the specified strategy.
        /// </summary>
        /// <param name="uiEntity">Window to hide.</param>
        /// <param name="hideStrategy">How a window must be hidden.</param>
        void Hide(UIEntity uiEntity, IHideInHierarchyStrategy hideStrategy);

        /// <summary>
        /// Hides a window with the default strategy.
        /// </summary>
        /// <param name="uiEntity">Window to hide.</param>
        void Hide(UIEntity uiEntity);
        
        /// <summary>
        /// Returns first met window (from top to bottom of the hierarchy) with specified type.
        /// </summary>
        /// <typeparam name="T">Type of searching window.</typeparam>
        /// <returns>Window in hierarchy.</returns>
        T GetWindow<T>() 
            where T : MonoBehaviour, IUISystemEntity;
        
        /// <summary>
        /// Returns highest window in the hierarchy.
        /// </summary>
        /// <returns>Highest window in the hierarchy.</returns>
        IUISystemEntity GetWindowOnTop();
        
        /// <summary>
        /// Returns true if WindowsSystem has scheduled (not executed yet) commands.
        /// </summary>
        /// <returns>True, if system has not executed commands.</returns>
        bool HasPendingCommands();
    }
}


