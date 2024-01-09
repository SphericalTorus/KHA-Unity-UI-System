using Kha.UI.Core;
using UnityEngine;

namespace Kha.UI.Demo
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UISystem _uiSystem;
        [SerializeField] private Camera _worldCamera;
        [SerializeField] private HelpGiver _helpGiver;
        [SerializeField] private Building _building;

        private void Start()
        {
            _uiSystem.Initialize(_worldCamera);
            _uiSystem.Windows.Show<GameplayWindow>().OnCreated(window => window.Init(_uiSystem));
            
            _helpGiver.Init(_uiSystem);
            _building.Init(_uiSystem);
        }
    }
}