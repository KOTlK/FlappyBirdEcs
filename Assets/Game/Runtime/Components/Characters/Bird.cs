using Game.Runtime.Services.Rendering.Bird;

namespace Game.Runtime.Components.Characters
{
    public struct Bird
    {
        public int Score { get; set; }
        public IBirdRenderer Renderer { get; set; }
    }
}