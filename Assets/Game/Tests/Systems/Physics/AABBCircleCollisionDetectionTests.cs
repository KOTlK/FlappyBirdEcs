using Game.Runtime.Systems.Physics;
using Leopotam.EcsLite;
using NUnit.Framework;
using UnityEngine;
using Collision = Game.Runtime.Components.Physics.Collision;

namespace Game.Tests.Systems.Physics
{
    public class AABBCircleCollisionDetectionTests
    {
        [Test]
        public void CollideAABBAndCircle()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var filter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitAABBCollidersSystem(new[]
                {
                    (Vector2.zero, Vector2.one)
                }))
               .Add(new FakeInitMultipleCircleCollidersSystem(new[]
                {
                    (new Vector2(2, 0), 0.5f)
                }))
               .Add(new FakeMoveMultipleObjectsSystem(new[]
                {
                    Vector2.zero,
                    new Vector2(-1f, 0)
                }))
               .Add(new AABBCircleCollisionDetectionSystem())
               .Init();

            systems.Run();
            
            Assert.True(filter.GetEntitiesCount() == 1);
        }

        [Test]
        public void CollideCircleAndAABBAndClearPool()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var filter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitAABBCollidersSystem(new []
                {
                    (Vector2.zero, Vector2.one)
                }))
               .Add(new FakeInitMultipleCircleCollidersSystem(new[] 
                {
                    (new Vector2(2, 0), 0.5f)
                }))
               .Add(new FakeMoveMultipleObjectsSystem(new[]
                {
                    Vector2.zero,
                    new Vector2(-1f, 0)
                }))
               .Add(new AABBCircleCollisionDetectionSystem())
               .Init();
            
            systems.Run();
            systems.Run();
            systems.Run();
            systems.Run();
            
            Assert.True(filter.GetEntitiesCount() == 0);
        }

        [Test]
        public void CollideMultipleCirclesAndAABB()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var filter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitAABBCollidersSystem(new[]
                {
                    (new Vector2(-2, 0), Vector2.one),
                    (new Vector2(2, 0), Vector2.one)
                }))
               .Add(new FakeInitMultipleCircleCollidersSystem(new[]
                {
                    (new Vector2(0, 2), 0.5f),
                    (new Vector2(0, -2), 0.5f)
                }))
               .Add(new FakeMoveMultipleObjectsSystem(new[]
                {
                    new Vector2(1, 0),
                    new Vector2(-1, 0),
                    new Vector2(0, -1),
                    new Vector2(0, 1)
                }))
               .Add(new AABBCircleCollisionDetectionSystem())
               .Init();
            
            systems.Run();
            systems.Run();
            
            Assert.True(filter.GetEntitiesCount() == 4);
        }
        
        [Test]
        public void CollideMultipleCirclesAndAABBAndClearPool()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var filter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitAABBCollidersSystem(new[]
                {
                    (new Vector2(-2, 0), Vector2.one),
                    (new Vector2(2, 0), Vector2.one)
                }))
               .Add(new FakeInitMultipleCircleCollidersSystem(new[]
                {
                    (new Vector2(0, 2), 0.5f),
                    (new Vector2(0, -2), 0.5f)
                }))
               .Add(new FakeMoveMultipleObjectsSystem(new[]
                {
                    new Vector2(1, 0),
                    new Vector2(-1, 0),
                    new Vector2(0, -1),
                    new Vector2(0, 1)
                }))
               .Add(new AABBCircleCollisionDetectionSystem())
               .Init();
            
            systems.Run();
            systems.Run();
            systems.Run();
            
            Assert.True(filter.GetEntitiesCount() == 0);
        }
    }
}