using Game.Runtime.Components;
using Game.Runtime.Components.Events.Commands;
using Game.Runtime.Components.Physics;
using Game.Runtime.Components.Pipes;
using Game.Runtime.Startup;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Pipes
{
    public class SpawnCommandsSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var shared = systems.GetShared<Shared>();
            var screen = shared.Screen;

            var commandsPool = world.GetPool<SpawnPipesCommand>();
            var lastSpawnedPool = world.GetPool<LastSpawned>();
            var transforms = world.GetPool<Transform>();
            var pipeGroupsPool = world.GetPool<PipeGroup>();
            var lastSpawnedPipeGroups = world.Filter<PipeGroup>().Inc<LastSpawned>().End();

            foreach (var entity in lastSpawnedPipeGroups)
            {
                ref var group = ref pipeGroupsPool.Get(entity);
                ref var transform = ref transforms.Get(group.Score);

                var viewportPosition = screen.WorldToViewport(transform.Position);
                
                if (viewportPosition.x <= 0.4f)
                {
                    commandsPool.Add(world.NewEntity());

                    lastSpawnedPool.Del(entity);
                }
            }
        }
    }
}