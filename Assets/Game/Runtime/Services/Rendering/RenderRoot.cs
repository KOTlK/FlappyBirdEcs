using Game.Runtime.Services.Rendering.Pipes;

namespace Game.Runtime.Services.Rendering
{
    public class RenderRoot : IRenderRoot
    {
        public IPipeFactory PipesFactory { get; set; }
    }
}