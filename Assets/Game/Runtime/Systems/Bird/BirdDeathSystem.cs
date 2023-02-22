using Game.Runtime.Components.Pipes;
using Leopotam.EcsLite;
using Collision = Game.Runtime.Components.Physics.Collision;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;

namespace Game.Runtime.Systems.Events.Bird
{
    public class BirdDeathSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var rigidbodiesPool = world.GetPool<Rigidbody>();
            var birdsPool = world.GetPool<Components.Characters.Bird>();
            var collisionsPool = world.GetPool<Collision>();
            var deadPool = world.GetPool<Dead>();
            var pipesPool = world.GetPool<Pipe>();
            var collisionsFilter = world.Filter<Collision>().End();
            
            foreach (var entity in collisionsFilter)
            {
                ref var collision = ref collisionsPool.Get(entity);

                if (collision.IsCollisionBetween(birdsPool, pipesPool, out var birdEntity, out var pipeEntity))
                {
                    if (deadPool.Has(birdEntity))
                        continue;

                    ref var rigidbody = ref rigidbodiesPool.Get(birdEntity);

                    rigidbody.AffectedByPhysics = false;
                    deadPool.Add(birdEntity);
                }
            }
        }
    }
}