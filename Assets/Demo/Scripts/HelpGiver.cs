using Kha.UI.Core;
using UnityEngine;

namespace Kha.UI.Demo
{
    public class HelpGiver : MonoBehaviour
    {
        [SerializeField] private UIOwner _uiOwner;
        
        public void Init(UISystem uiSystem)
        {
            uiSystem.WorldWindows.Show<HelpWorldWindow>(_uiOwner).OnCreated(
                window => window.Init(uiSystem, _uiOwner));
        }
    }
}
