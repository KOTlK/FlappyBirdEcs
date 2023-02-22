using Game.Runtime.Components.Physics;
using Leopotam.EcsLite;
using UnityEngine;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Tests.Systems.Physics
{
    public class FakeMoveCollidersSystem : IEcsRunSystem
    {
        private readonly Vector2 _firstMovePerFrame;
        private readonly Vector2 _secondMovePerFrame;

        public FakeMoveCollidersSystem(Vector2 firstMovePerFrame, Vector2 secondMovePerFrame)
        {
            _firstMovePerFrame = firstMovePerFrame;
            _secondMovePerFrame = secondMovePerFrame;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var transformPool = world.GetPool<Transform>();
            
            var firstFilter = world.Filter<FakeFirstMarker>()
                                   .Inc<Transform>()
                                   .Inc<Rigidbody>()
                                   .Inc<CircleCollider>()
                                   .End();
            
            var secondFilter = world.Filter<FakeSecondMarker>()
                                   .Inc<Transform>()
                                   .Inc<Rigidbody>()
                                   .Inc<CircleCollider>()
                                   .End();

            foreach (var entity in firstFilter)
            {
                ref var transform = ref transformPool.Get(entity);
                transform.Position += _firstMovePerFrame;
            }

            foreach (var entity in secondFilter)
            {
                ref var transform = ref transformPool.Get(entity);
                transform.Position += _secondMovePerFrame;
            }
        }
    }
}