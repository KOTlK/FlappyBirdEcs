using Game.Runtime.Components.Physics;
using Game.Runtime.Components.Pipes;
using Game.Runtime.Startup;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Pipes
{
    public class PipesPoolingSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var screen = systems.GetShared<Shared>().Screen;
            var pooledPool = world.GetPool<Pooled>();
            var transformsPool = world.GetPool<Transform>();
            var aabbsPool = world.GetPool<AABB>();
            var pipeGroupsPool = world.GetPool<PipeGroup>();
            var unpooledPipeGroups = world.Filter<PipeGroup>().Exc<Pooled>().End();

            foreach (var entity in unpooledPipeGroups)
            {
                ref var group = ref pipeGroupsPool.Get(entity);
                ref var transform = ref transformsPool.Get(group.Score);
                ref var aabb = ref aabbsPool.Get(group.Score);
                
                var worldPosition = transform.Position + aabb.HalfExtents;
                var viewportPosition = screen.WorldToViewport(worldPosition);

                if (viewportPosition.x < 0)
                {
                    pooledPool.Add(entity);
                }
            }
        }
    }
}