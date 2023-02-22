using Leopotam.EcsLite;
using UnityEngine;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;

namespace Game.Runtime.Systems.Events.Bird.Movement
{
    public class BirdRotationSystem : IEcsRunSystem
    {
        private const float MaxVelocity = 3f;
        private const float MaxAngle = 45f;
        
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

                var velocity = Mathf.Clamp(rigidbody.Velocity.y, -MaxVelocity, MaxVelocity);

                var t = (velocity - -MaxVelocity) / (MaxVelocity - -MaxVelocity);
                var maxRotation = Quaternion.AngleAxis(MaxAngle, new Vector3(0, 0, 1));
                var minRotation = Quaternion.AngleAxis(-MaxAngle, new Vector3(0, 0, 1));
                var rotation = Quaternion.Slerp(minRotation, maxRotation, t);

                bird.Renderer.Rotation = rotation;
            }
        }
        
    }
}