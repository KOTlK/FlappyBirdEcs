using Game.Runtime.Components.Physics;
using Game.Runtime.Startup;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Physics
{
    public class ApplyPhysicsSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var time = systems.GetShared<Shared>().Time;
            var rigidbodiesPool = world.GetPool<Rigidbody>();
            var transformsPool = world.GetPool<Transform>();
            var filter = world.Filter<Rigidbody>().Inc<Transform>().End();

            foreach (var entity in filter)
            {
                ref var rigidbody = ref rigidbodiesPool.Get(entity);
                ref var transform = ref transformsPool.Get(entity);

                if (rigidbody.AffectedByPhysics == false)
                    continue;
                transform.Position += rigidbody.Velocity * time.FixedDeltaTime;
            }
        }
    }
}