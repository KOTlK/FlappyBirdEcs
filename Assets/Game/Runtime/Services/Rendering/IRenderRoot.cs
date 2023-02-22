using Game.Runtime.Services.Rendering.Pipes;

namespace Game.Runtime.Services.Rendering
{
    public interface IRenderRoot
    {
        IPipeFactory PipesFactory { get; set; }
    }
}