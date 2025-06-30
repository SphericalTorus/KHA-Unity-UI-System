namespace Kha.UI.Core
{
    /// <summary>
    /// Outer interface of the UI system part, which is responsible for world windows logic.
    /// </summary>
    public interface IWorldWindowsSystem
    {
        /// <summary>
        /// Shows a window with the specified strategy and attaches it to a world space object.
        /// </summary>
        /// <param name="uiOwner">Object to be attached to.</param>
        /// <param name="showStrategy">How window must be shown.</param>
        /// <typeparam name="T">Type of showing window.</typeparam>
        /// <returns>Command interface to specify additional options.</returns>
        IShowUIEntityCommand<T> Show<T>(UIOwner uiOwner, IShowInHierarchyStrategy showStrategy) 
            where T : WorldWindow;
        
        /// <summary>
        /// Shows a window with the default strategy and attaches it to a world space object.
        /// </summary>
        /// <param name="uiOwner">Object to be attached to.</param>
        /// <typeparam name="T">Type of showing window.</typeparam>
        /// <returns>Command interface to specify additional options.</returns>
        IShowUIEntityCommand<T> Show<T>(UIOwner uiOwner) 
            where T : WorldWindow;
        
        /// <summary>
        /// Checks if a window is shown and attached to the specified world object.
        /// </summary>
        /// <param name="uiOwner">World object.</param>
        /// <typeparam name="T">Type of checking window.</typeparam>
        /// <returns>Is world window shown.</returns>
        bool IsWorldWindowShown<T>(UIOwner uiOwner) 
            where T : WorldWindow;
        
        /// <summary>
        /// Hides a window of the specified type from world object's hierarchy with a custom strategy.
        /// </summary>
        /// <param name="uiOwner">World object.</param>
        /// <param name="hideStrategy">How window must be hidden.</param>
        /// <typeparam name="T">Type of hiding window.</typeparam>
        void Hide<T>(UIOwner uiOwner, IHideInHierarchyStrategy hideStrategy) 
            where T : WorldWindow;
        
        /// <summary>
        /// Hides a window of the specified type from world object's hierarchy with the default strategy.
        /// </summary>
        /// <param name="uiOwner">World object.</param>
        /// <typeparam name="T">Type of hiding window.</typeparam>
        void Hide<T>(UIOwner uiOwner) 
            where T : WorldWindow;

        /// <summary>
        /// Hides a window from world object's hierarchy with a custom strategy.
        /// </summary>
        /// <param name="uiEntity">World window to hide.</param>
        /// <param name="uiOwner">World object.</param>
        /// <param name="hideStrategy">How window must be hidden.</param>
        void Hide(UIEntity uiEntity, UIOwner uiOwner, IHideInHierarchyStrategy hideStrategy);

        /// <summary>
        /// Hides a window of the specified type from world object's hierarchy with the default strategy.
        /// </summary>
        /// <param name="uiEntity">World window to hide.</param>
        /// <param name="uiOwner">World object.</param>
        void Hide(UIEntity uiEntity, UIOwner uiOwner);
        
        /// <summary>
        /// Hides all open world windows for all UI owners.
        /// </summary>
        void HideAll();
        
        /// <summary>
        /// Recalculates positions of all world windows.
        /// </summary>
        void ForceUpdatePositions();
    }
}