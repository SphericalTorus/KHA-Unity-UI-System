using System;
using UnityEngine;

namespace Kha.UI.Core
{
    public interface IUIPool
    {
        T Get<T>() where T : MonoBehaviour, IUISystemEntity;
        IUISystemEntity Get(Type entityType);
        void Release(IUISystemEntity entity);
    }
}
