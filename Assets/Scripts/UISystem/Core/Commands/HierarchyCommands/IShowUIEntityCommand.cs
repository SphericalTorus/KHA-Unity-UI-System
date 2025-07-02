using System;
using UnityEngine;

namespace Kha.UI.Core
{
    /// <summary>
    /// Provides a set of additional options, which can be applied to the process of an entity show.
    /// </summary>
    /// <typeparam name="T">Type of showing entity.</typeparam>
    public interface IShowUIEntityCommand<out T> where T : MonoBehaviour, IUISystemEntity
    {
        /// <summary>
        /// Overrides appearance animation.
        /// </summary>
        /// <typeparam name="TAppearanceAnimation">Type of custom appearance animation.</typeparam>
        IShowUIEntityCommand<T> AppearWith<TAppearanceAnimation>() where TAppearanceAnimation : UIAnimationBase;
        
        /// <summary>
        /// Overrides disappearance animation.
        /// </summary>
        /// <typeparam name="TDisappearanceAnimation">Type of custom disappearance animation.</typeparam>
        IShowUIEntityCommand<T> DisappearWith<TDisappearanceAnimation>() where TDisappearanceAnimation : UIAnimationBase;
        
        /// <summary>
        /// Callback with just created, but not shown yet UI entity.
        /// </summary>
        /// <param name="onCreated"></param>
        /// <returns>Created UI entity.</returns>
        IShowUIEntityCommand<T> OnCreated(Action<T> onCreated);
    }
}