using Kha.UI.Core;
using UnityEngine;

namespace Kha.UI.Demo
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private UIOwner _uiOwner;

        private UISystem _uiSystem;
        
        public void Init(UISystem uiSystem)
        {
            _uiSystem = uiSystem;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterMovement>() != null)
            {
                _uiSystem.WorldWindows.Show<BuildingConstructionWorldWindow>(_uiOwner).OnCreated(
                    window => window.Init(_uiSystem, _uiOwner));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<CharacterMovement>() != null)
            {
                _uiSystem.WorldWindows.Hide<BuildingConstructionWorldWindow>(_uiOwner);
            }
        }
    }
}