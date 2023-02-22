using Game.Runtime.Components.Physics;
using Leopotam.EcsLite;
using UnityEngine;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Tests.Systems.Physics
{
    public class FakeInitMultipleCircleCollidersSystem : IEcsInitSystem
    {
        private readonly (Vector2, float)[] _colliders;

        public FakeInitMultipleCircleCollidersSystem((Vector2, float)[] colliders)
        {
            _colliders = colliders;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var collidersPool = world.GetPool<CircleCollider>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            var transformPool = world.GetPool<Transform>();

            foreach (var (position, radius) in _colliders)
            {
                var entity = world.NewEntity();
                ref var collider = ref collidersPool.Add(entity);
                ref var transform = ref transformPool.Add(entity);
                rigidbodyPool.Add(entity);

                collider.Radius = radius;
                transform.Position = position;
            }
        }
    }
}