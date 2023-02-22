using Game.Runtime.Services.UI.Elements;
using Game.Runtime.Services.UI.InGame;
using Game.Runtime.Services.UI.Lose;
using Game.Runtime.Services.UI.MainMenu;

namespace Game.Runtime.Services.UI
{
    public interface IUIRoot : IElement
    {
        IMainMenu MainMenu { get; }
        IInGame InGame { get; }
        ILoseScreen LoseScreen { get; }
    }
}