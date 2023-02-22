using Game.Runtime.Components.Events.Commands;
using Game.Runtime.Components.Physics;
using Game.Runtime.Components.Pipes;
using Game.Runtime.Startup;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Pipes
{
    public class SpawnPooledPipesSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var shared = systems.GetShared<Shared>();
            var screen = shared.Screen;
            var difficulty = shared.StaticData.Difficulty;
            var spawnCommands = world.Filter<SpawnPipesCommand>().End();
            var commandsPool = world.GetPool<SpawnPipesCommand>();
            var transformsPool = world.GetPool<Transform>();
            var pipeGroupsPool = world.GetPool<PipeGroup>();
            var pool = world.GetPool<Pooled>();
            var lastSpawnedPool = world.GetPool<LastSpawned>();
            var pooledPipeGroups = world.Filter<PipeGroup>().Inc<Pooled>().End();

            foreach (var spawnCommand in spawnCommands)
            {
                if (pooledPipeGroups.GetEntitiesCount() < 1) break;

                var pipeGroupEntity = pooledPipeGroups.GetRawEntities()[0];
                var upperPipePosition = PipesUtils.GetWorldPosition(true, screen, difficulty);
                var lowerPipePosition = PipesUtils.GetWorldPosition(false, screen, difficulty);
                var scorePosition = PipesUtils.GetScorePosition(screen, difficulty);

                ref var pipeGroup = ref pipeGroupsPool.Get(pipeGroupEntity);
                ref var upperPipeTransform = ref transformsPool.Get(pipeGroup.UpperPipe);
                ref var lowerPipeTransform = ref transformsPool.Get(pipeGroup.LowerPipe);
                ref var scoreTransform = ref transformsPool.Get(pipeGroup.Score);

                upperPipeTransform.Position = upperPipePosition;
                lowerPipeTransform.Position = lowerPipePosition;
                scoreTransform.Position = scorePosition;

                pool.Del(pipeGroupEntity);
                lastSpawnedPool.Add(pipeGroupEntity);
                
                commandsPool.Del(spawnCommand);
            }
        }
    }
}