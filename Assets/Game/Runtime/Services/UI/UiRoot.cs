using Game.Runtime.Services.UI.Elements;
using Game.Runtime.Services.UI.InGame;
using Game.Runtime.Services.UI.Lose;
using Game.Runtime.Services.UI.MainMenu;
using UnityEngine;

namespace Game.Runtime.Services.UI
{
    public class UiRoot : UIElement, IUIRoot
    {
        [SerializeField] private MainMenu.MainMenu _mainMenu;
        [SerializeField] private InGameUI _inGame;
        [SerializeField] private LoseScreen _loseScreen;

        public IMainMenu MainMenu => _mainMenu;
        public IInGame InGame => _inGame;
        public ILoseScreen LoseScreen => _loseScreen;
    }
}