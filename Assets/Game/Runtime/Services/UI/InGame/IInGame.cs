using Game.Runtime.Services.UI.Elements;

namespace Game.Runtime.Services.UI.InGame
{
    public interface IInGame : IElement
    {
        IText Score { get; }
    }
}