using Game.Runtime.Components.Physics;
using Game.Runtime.Startup;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Physics
{
    public class ApplyGravitySystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var physicsConfig = systems.GetShared<Shared>().StaticData.PhysicsConfig;
            var time = systems.GetShared<Shared>().Time;
            var filter = world.Filter<Rigidbody>().Inc<Transform>().End();
            var rigidbodiesPool = world.GetPool<Rigidbody>();

            foreach (var entity in filter)
            {
                ref var rigidbody = ref rigidbodiesPool.Get(entity);
                
                if (rigidbody.AffectedByPhysics == false)
                    continue;

                if (rigidbody.HasCustomGravity)
                {
                    rigidbody.Velocity += rigidbody.CustomGravity * time.FixedDeltaTime;
                    continue;
                }
                
                rigidbody.Velocity += physicsConfig.Gravity * time.FixedDeltaTime;
            }
        }
    }
}