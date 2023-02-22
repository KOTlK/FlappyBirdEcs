using Game.Runtime.Components.Characters;
using Game.Runtime.Components.Physics;
using Game.Runtime.Startup;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Score
{
    public class ScoreSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var ui = systems.GetShared<Shared>().UserInterface;
            var birdsPool = world.GetPool<Bird>();
            var scoresPool = world.GetPool<Components.Score>();
            var triggersPool = world.GetPool<TriggerCollision>();
            var triggersFilter = world.Filter<TriggerCollision>().End();

            foreach (var entity in triggersFilter)
            {
                ref var trigger = ref triggersPool.Get(entity);

                if (trigger.ExecutedThisFrame == false)
                {
                    continue;
                }

                if (trigger.IsCollisionBetween(birdsPool, scoresPool, out var birdEntity, out var scoreEntity))
                {
                    ref var bird = ref birdsPool.Get(birdEntity);
                    ref var score = ref scoresPool.Get(scoreEntity);

                    bird.Score += score.Amount;
                    
                    ui.InGame.Score.SetString(bird.Score.ToString());
                }
            }
        }
    }
}