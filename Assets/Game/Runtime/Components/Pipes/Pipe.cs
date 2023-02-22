using Game.Runtime.Services.Rendering.Pipes;

namespace Game.Runtime.Components.Pipes
{
    public struct Pipe
    {
        public IPipeRenderer Renderer { get; set; }
    }
}