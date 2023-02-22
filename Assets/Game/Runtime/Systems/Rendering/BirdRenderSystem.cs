using Game.Runtime.Components.Characters;
using Game.Runtime.Components.Physics;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Rendering
{
    public class BirdRenderSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<Transform>()
                              .Inc<Bird>()
                              .End();
            var transformPool = world.GetPool<Transform>();
            var birdsPool = world.GetPool<Bird>();

            foreach (var entity in filter)
            {
                ref var transform = ref transformPool.Get(entity);
                ref var bird = ref birdsPool.Get(entity);

                bird.Renderer.Position = transform.Position;
            }
        }
    }
}