using Game.Runtime.Components.Physics;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Events.Bird
{
    public class BirdAnimationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var birdsPool = world.GetPool<Components.Characters.Bird>();
            var rigidbodiesPool = world.GetPool<Rigidbody>();
            var filter = world.Filter<Components.Characters.Bird>().Inc<Rigidbody>().End();

            foreach (var entity in filter)
            {
                ref var bird = ref birdsPool.Get(entity);
                ref var rigidbody = ref rigidbodiesPool.Get(entity);

                if (rigidbody.AffectedByPhysics == false)
                {
                    bird.Renderer.VelocityY = -110f;
                    return;
                }
                
                bird.Renderer.VelocityY = rigidbody.Velocity.y;
            }
        }
    }
}