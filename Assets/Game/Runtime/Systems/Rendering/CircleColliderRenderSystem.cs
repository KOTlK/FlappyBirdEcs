using Game.Runtime.Components.Physics;
using Game.Runtime.Components.Physics.Debug;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Rendering
{
    public class CircleColliderRenderSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var transformsPool = world.GetPool<Transform>();
            var renderersPool = world.GetPool<CircleRendering>();
            var filter = world.Filter<CircleCollider>().Inc<Transform>().Inc<CircleRendering>().End();

            foreach (var entity in filter)
            {
                ref var transform = ref transformsPool.Get(entity);
                ref var renderer = ref renderersPool.Get(entity);

                renderer.Renderer.SetPosition(transform.Position);
            }
        }
    }
}