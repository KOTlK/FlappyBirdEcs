using Game.Runtime.Services.Rendering.Physics;

namespace Game.Runtime.Components.Physics.Debug
{
    public struct AABBRendering
    {
        public IAABBDebugRender Renderer { get; set; }
    }
}