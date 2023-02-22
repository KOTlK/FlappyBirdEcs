using Game.Runtime.Components;
using Game.Runtime.Components.Events.Commands;
using Game.Runtime.Components.Pipes;
using Game.Runtime.Components.Physics;
using Game.Runtime.Services.Rendering.Pipes;
using Game.Runtime.Startup;
using Leopotam.EcsLite;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Runtime.Systems.Pipes
{
    public class PipesSpawnSystem : IEcsRunSystem
    {
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var shared = systems.GetShared<Shared>();
            var screen = shared.Screen;
            var difficulty = shared.StaticData.Difficulty;
            var factory = shared.RenderRoot.PipesFactory;
            var lastSpawnedPool = world.GetPool<LastSpawned>();
            var spawnCommands = world.Filter<SpawnPipesCommand>().End();
            var commandsPool = world.GetPool<SpawnPipesCommand>();
            var pipesPool = world.GetPool<Pipe>();
            var scorePool = world.GetPool<Components.Score>();
            var transformsPool = world.GetPool<Transform>();
            var aabbPool = world.GetPool<AABB>();
            var pipeGroupsPool = world.GetPool<PipeGroup>();

            foreach (var spawnCommand in spawnCommands)
            {
                var upperPipeEntity = world.NewEntity();
                var lowerPipeEntity = world.NewEntity();
                var scoreEntity = world.NewEntity();
                var pipeGroupEntity = world.NewEntity();

                ref var pipe = ref pipesPool.Add(upperPipeEntity);
                ref var secondPipe = ref pipesPool.Add(lowerPipeEntity);
                ref var transform = ref transformsPool.Add(upperPipeEntity);
                ref var secondTransform = ref transformsPool.Add(lowerPipeEntity);
                ref var aabb = ref aabbPool.Add(upperPipeEntity);
                ref var secondAabb = ref aabbPool.Add(lowerPipeEntity);
                ref var score = ref scorePool.Add(scoreEntity);
                ref var scoreAabb = ref aabbPool.Add(scoreEntity);
                ref var scoreTransform = ref transformsPool.Add(scoreEntity);
                ref var pipeGroup = ref pipeGroupsPool.Add(pipeGroupEntity);

                var scorePosition = PipesUtils.GetScorePositionAndSize(screen, difficulty, out var scoreSize);
                var upperPipe = PipesUtils.GeneratePipe(factory, screen, difficulty, out var upperSize);
                var lowerPipe = PipesUtils.GeneratePipe(factory, screen, difficulty, out var lowerSize, false);
                upperPipe.SetMaterial(IPipeRenderer.PipeMaterial.UpperPipe);
                lowerPipe.SetMaterial(IPipeRenderer.PipeMaterial.LowerPipe);

                transform.Position = upperPipe.Position;
                aabb.Size = upperSize;
                pipe.Renderer = upperPipe;
                secondTransform.Position = lowerPipe.Position;
                secondAabb.Size = lowerSize;
                secondPipe.Renderer = lowerPipe;
                score.Amount = difficulty.ScorePerPipe;

                scoreAabb.Size = scoreSize;
                scoreAabb.IsTrigger = true;
                scoreTransform.Position = scorePosition;

                pipeGroup.UpperPipe = upperPipeEntity;
                pipeGroup.LowerPipe = lowerPipeEntity;
                pipeGroup.Score = scoreEntity;

                lastSpawnedPool.Add(pipeGroupEntity);
                commandsPool.Del(spawnCommand); 
            }
        }
    }
}