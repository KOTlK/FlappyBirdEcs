using Game.Runtime.Components.Physics;
using Leopotam.EcsLite;
using UnityEngine;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Tests.Systems.Physics
{
    public class FakeInitCircleCollidersSystem : IEcsInitSystem
    {
        private readonly float _firstColliderRadius;
        private readonly float _secondColliderRadius;
        private readonly Vector2 _firstInitPosition;
        private readonly Vector2 _secondInitPosition;

        public FakeInitCircleCollidersSystem(float firstColliderRadius, float secondColliderRadius, Vector2 firstInitPosition, Vector2 secondInitPosition)
        {
            _firstColliderRadius = firstColliderRadius;
            _secondColliderRadius = secondColliderRadius;
            _firstInitPosition = firstInitPosition;
            _secondInitPosition = secondInitPosition;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var collidersPool = world.GetPool<CircleCollider>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            var transformPool = world.GetPool<Transform>();
            var fakeFirstPool = world.GetPool<FakeFirstMarker>();
            var fakeSecondPool = world.GetPool<FakeSecondMarker>();

            var firstEntity = world.NewEntity();
            var secondEntity = world.NewEntity();

            ref var firstCollider = ref collidersPool.Add(firstEntity);
            ref var secondCollider = ref collidersPool.Add(secondEntity);

            ref var firstRigidbody = ref rigidbodyPool.Add(firstEntity);
            ref var secondRigidbody = ref rigidbodyPool.Add(secondEntity);

            ref var firstTransform = ref transformPool.Add(firstEntity);
            ref var secondTransform = ref transformPool.Add(secondEntity);

            firstCollider.Radius = _firstColliderRadius;
            secondCollider.Radius = _secondColliderRadius;
            firstTransform.Position = _firstInitPosition;
            secondTransform.Position = _secondInitPosition;
            firstRigidbody.Velocity = Vector2.zero;
            secondRigidbody.Velocity = Vector2.zero;

            fakeFirstPool.Add(firstEntity);
            fakeSecondPool.Add(secondEntity);
        }
    }
}