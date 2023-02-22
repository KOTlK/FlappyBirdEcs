using Game.Runtime.Systems.Physics;
using Leopotam.EcsLite;
using NUnit.Framework;
using UnityEngine;
using Collision = Game.Runtime.Components.Physics.Collision;

namespace Game.Tests.Systems.Physics
{
    public class AABBCollisionDetectionTests
    {
        [Test]
        public void CollideTwoAABB()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var collisionsFilter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitAABBCollidersSystem(new (Vector2 position, Vector2 size)[]
                {
                    (Vector2.zero, new Vector2(1f, 1f)),
                    (new Vector2(2, 0), new Vector2(1f, 1f))
                }))
               .Add(new FakeMoveMultipleObjectsSystem(new Vector2[]
                    {
                        new (0.5f, 0),
                        new (-0.5f, 0)
                    }))
               .Add(new AABBCollisionDetectionSystem())
               .Init();

            Assert.True(collisionsFilter.GetEntitiesCount() == 0);

            systems.Run();

            Assert.True(collisionsFilter.GetEntitiesCount() == 1);
        }

        [Test]
        public void CollideTwoAABBAndClearPool()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var collisionsFilter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitAABBCollidersSystem(new (Vector2 position, Vector2 size)[]
                {
                    (Vector2.zero, new Vector2(1f, 1f)),
                    (new Vector2(2, 0), new Vector2(1f, 1f))
                }))
               .Add(new FakeMoveMultipleObjectsSystem(new Vector2[]
                {
                    new (0.5f, 0),
                    new (-0.5f, 0)
                }))
               .Add(new AABBCollisionDetectionSystem())
               .Init();

            systems.Run();
            systems.Run();
            systems.Run();
            systems.Run();

            Assert.True(collisionsFilter.GetEntitiesCount() == 0);
        }

        [Test]
        public void CollideMultipleAABB()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var collisionsFilter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitAABBCollidersSystem(new (Vector2 position, Vector2 size)[]
                {
                    (Vector2.zero, new Vector2(1f, 1f)),
                    (new Vector2(2f, 0f), new Vector2(1f, 1f)),
                    (new Vector2(0f, -2f), new Vector2(1f, 1f))
                }))
               .Add(new FakeMoveMultipleObjectsSystem(new Vector2[]
                {
                    new (0.5f, 0f),
                    new (-0.5f, 0f),
                    new (1, 1)
                    
                }))
               .Add(new AABBCollisionDetectionSystem())
               .Init();

            Assert.True(collisionsFilter.GetEntitiesCount() == 0);

            systems.Run();

            Assert.True(collisionsFilter.GetEntitiesCount() == 3);
        }

        [Test]
        public void CollideMultipleAABBAndClearPool()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var collisionsFilter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitAABBCollidersSystem(new (Vector2 position, Vector2 size)[]
                {
                    (Vector2.zero, new Vector2(1f, 1f)),
                    (new Vector2(2f, 0f), new Vector2(1f, 1f)),
                    (new Vector2(0f, -2f), new Vector2(1f, 1f))
                }))
               .Add(new FakeMoveMultipleObjectsSystem(new Vector2[]
                {
                    new (0.5f, 0f),
                    new (-0.5f, 0f),
                    new (1, 1)
                    
                }))
               .Add(new AABBCollisionDetectionSystem())
               .Init();

            systems.Run();
            systems.Run();
            systems.Run();
            systems.Run();

            Assert.True(collisionsFilter.GetEntitiesCount() == 0);
        }
    }
}