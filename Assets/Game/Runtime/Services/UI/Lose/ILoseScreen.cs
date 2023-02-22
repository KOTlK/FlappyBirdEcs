using Game.Runtime.Services.UI.Elements;

namespace Game.Runtime.Services.UI.Lose
{
    public interface ILoseScreen : IElement
    {
        IText CurrentScore { get; }
        IText HighScore { get; }
        IButton Restart { get; }
    }
}