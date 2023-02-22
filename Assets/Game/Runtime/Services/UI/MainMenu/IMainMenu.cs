using Game.Runtime.Services.UI.Elements;

namespace Game.Runtime.Services.UI.MainMenu
{
    public interface IMainMenu : IElement
    {
        IButton StartGame { get; }
    }
}