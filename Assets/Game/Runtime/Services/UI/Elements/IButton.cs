namespace Game.Runtime.Services.UI.Elements
{
    public interface IButton : IElement
    {
        bool Clicked { get; set; }
        void Reset();
    }
}