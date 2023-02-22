using Game.Runtime.Components.Pipes;
using Game.Runtime.Components.Physics;
using Game.Runtime.Startup;
using Leopotam.EcsLite;
using UnityEngine;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;
using Transform = Game.Runtime.Components.Physics.Transform;
using BirdComponent = Game.Runtime.Components.Characters.Bird;

namespace Game.Runtime.Systems.Events.Bird
{
    public class BirdSpawnSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var shared = systems.GetShared<Shared>();
            var controlledByPlayer = world.GetPool<ControlledByPlayer>();
            var colliderPool = world.GetPool<CircleCollider>();
            var transformPool = world.GetPool<Transform>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            var birdsPool = world.GetPool<BirdComponent>();
            var staticData = shared.StaticData;
            var obj = Object.Instantiate(staticData.BirdPrefab, Vector3.zero, Quaternion.identity);
            var entity = world.NewEntity();

            ref var collider = ref colliderPool.Add(entity);
            ref var transform = ref transformPool.Add(entity);
            ref var bird = ref birdsPool.Add(entity);
            ref var rigidbody = ref rigidbodyPool.Add(entity);

            rigidbody.AffectedByPhysics = false;
            rigidbody.HasCustomGravity = true;
            rigidbody.CustomGravity = new Vector2(0, -shared.StaticData.Difficulty.BirdFallSpeed);
            controlledByPlayer.Add(entity);

            bird.Renderer = obj;
            collider.Radius = shared.StaticData.BirdColliderRadius;
            transform.Position = new Vector2(-2f, 0);
        }
    }
}