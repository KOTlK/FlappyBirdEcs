using Game.Runtime.Services;
using Game.Runtime.Services.Rendering;
using Game.Runtime.Services.Rendering.Background;
using Game.Runtime.Services.Rendering.Physics;
using Game.Runtime.Services.Score;
using Game.Runtime.Services.UI;

namespace Game.Runtime.Startup
{
    public class Shared
    {
        public StaticData StaticData { get; set; }
        public Time Time { get; set; }
        public IRenderRoot RenderRoot { get; set; }
        public IScreen Screen { get; set; }
        public IUIRoot UserInterface { get; set; }
        public IGameScore Score { get; set; }
        public IBackground Background { get; set; }
        public AABBDebugFactory DebugAABB { get; set; }
        public ColliderDebugFactory CircleDebugFactory { get; set; }
    }
}