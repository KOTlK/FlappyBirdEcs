using Game.Runtime.Services.UI.Elements;
using UnityEngine;

namespace Game.Runtime.Services.UI.MainMenu
{
    public class MainMenu : UIElement, IMainMenu
    {
        [SerializeField] private Button _startGame;

        public IButton StartGame => _startGame;
    }
}