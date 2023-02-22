using Game.Runtime.Components.Physics;
using Game.Runtime.Components.Physics.Debug;
using Game.Runtime.Startup;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Rendering
{
    public class CircleColliderRenderSpawnSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var factory = systems.GetShared<Shared>().CircleDebugFactory;
            var collidersPool = world.GetPool<CircleCollider>();
            var renderersPool = world.GetPool<CircleRendering>();
            var filter = world.Filter<CircleCollider>().Exc<CircleRendering>().End();

            foreach (var entity in filter)
            {
                ref var collider = ref collidersPool.Get(entity);
                ref var renderer = ref renderersPool.Add(entity);

                renderer.Renderer = factory.Create(collider.Radius);
            }
        }
    }
}