using Game.Runtime.Components.Physics;
using Leopotam.EcsLite;
using UnityEngine;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Tests.Systems.Physics
{
    public class FakeInitAABBCollidersSystem : IEcsInitSystem
    {
        private readonly (Vector2 position, Vector2 size)[] _colliders;
        
        public FakeInitAABBCollidersSystem((Vector2 position, Vector2 size)[] colliders)
        {
            _colliders = colliders;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var collidersPool = world.GetPool<AABB>();
            var transformsPool = world.GetPool<Transform>();
            var rigidbodiesPool = world.GetPool<Rigidbody>();

            foreach (var collider in _colliders)
            {
                var entity = world.NewEntity();

                ref var newCollider = ref collidersPool.Add(entity);
                ref var transform = ref transformsPool.Add(entity);
                rigidbodiesPool.Add(entity);

                newCollider.Size = collider.size;
                transform.Position = collider.position;
            }
        }
    }
}