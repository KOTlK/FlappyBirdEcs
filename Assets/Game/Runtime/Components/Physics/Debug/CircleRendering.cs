using Game.Runtime.Services.Rendering.Physics;

namespace Game.Runtime.Components.Physics.Debug
{
    public struct CircleRendering
    {
        public ICircleColliderDebugRenderer Renderer { get; set; }
    }
}