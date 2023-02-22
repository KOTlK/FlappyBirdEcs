using Game.Runtime.Components;
using Game.Runtime.Components.Characters;
using Game.Runtime.Components.Pipes;
using Game.Runtime.Startup;
using Leopotam.EcsLite;
using UnityEngine;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Runtime.Systems.Pipes
{
    public class PipesMoveSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var shared = systems.GetShared<Shared>();
            var difficulty = shared.StaticData.Difficulty;
            var time = shared.Time;

            var transformsPool = world.GetPool<Transform>();
            var pipesFilter = world.Filter<Pipe>().Inc<Transform>().End();
            var scoresFilter = world.Filter<Components.Score>().Inc<Transform>().End();
            var undeadBirdsFilter = world.Filter<Bird>().Exc<Dead>().End();

            if (undeadBirdsFilter.GetEntitiesCount() == 0)
            {
                return;
            }
            
            foreach (var entity in pipesFilter)
            {
                ref var transform = ref transformsPool.Get(entity);

                transform.Position += new Vector2(-difficulty.PipesMovementSpeed, 0) * time.DeltaTime;
            }

            foreach (var entity in scoresFilter)
            {
                ref var transform = ref transformsPool.Get(entity);

                transform.Position += new Vector2(-difficulty.PipesMovementSpeed, 0) * time.DeltaTime;
            }
        }
    }
}