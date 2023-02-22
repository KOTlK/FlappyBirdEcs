using Game.Runtime.Components.Physics;
using Game.Runtime.Components.Pipes;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Rendering
{
    public class PipesRenderSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var transformPool = world.GetPool<Transform>();
            var pipesPool = world.GetPool<Pipe>();
            var pipesFilter = world.Filter<Pipe>().Inc<Transform>().End();

            foreach (var entity in pipesFilter)
            {
                ref var transform = ref transformPool.Get(entity);
                ref var pipe = ref pipesPool.Get(entity);
                
                pipe.Renderer.Position = transform.Position;
            }
        }
    }
}