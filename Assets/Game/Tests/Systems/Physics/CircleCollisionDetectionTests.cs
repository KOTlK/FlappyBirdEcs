using Game.Runtime.Systems.Physics;
using Leopotam.EcsLite;
using NUnit.Framework;
using UnityEngine;
using Collision = Game.Runtime.Components.Physics.Collision;

namespace Game.Tests.Systems.Physics
{
    public class CircleCollisionDetectionTests
    {
        [Test]
        public void CollideTwoCircles()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);

            systems
               .Add(new FakeInitCircleCollidersSystem(0.5f, 0.5f, Vector2.zero, new Vector2(4, 0)))
               .Add(new FakeMoveCollidersSystem(Vector2.zero, new Vector2(-1, 0)))
               .Add(new CircleCollisionDetectionSystem())
               .Init();

            systems.Run();
            systems.Run();
            systems.Run();

            var collisionsFilter = world.Filter<Collision>().End();
            
            Assert.True(collisionsFilter.GetEntitiesCount() == 1);
        }

        [Test]
        public void CollideTwoCirclesAndRemoveFromPool()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var collisionsFilter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitCircleCollidersSystem(0.5f, 0.5f, Vector2.zero, new Vector2(4, 0)))
               .Add(new FakeMoveCollidersSystem(Vector2.zero, new Vector2(-1, 0)))
               .Add(new CircleCollisionDetectionSystem())
               .Init();

            systems.Run();
            systems.Run();
            systems.Run();

            Assert.True(collisionsFilter.GetEntitiesCount() == 1);
            
            systems.Run();
            systems.Run();
            systems.Run();

            
            Assert.True(collisionsFilter.GetEntitiesCount() == 0);
        }

        [Test]
        public void CollideMultipleCircles()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var collisionsFilter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitMultipleCircleCollidersSystem(new []
                {
                    (Vector2.zero, 0.5f),
                    (new Vector2(2, 0), 0.5f),
                    (new Vector2(0, -2), 0.5f)
                }))
               .Add(new FakeMoveMultipleObjectsSystem(new[]
                {
                    new Vector2(0, 0),
                    new Vector2(-1, 0),
                    new Vector2(0, 1)
                }))
               .Add(new CircleCollisionDetectionSystem())
               .Init();
            
            systems.Run();
            systems.Run();

            Assert.True(collisionsFilter.GetEntitiesCount() == 3);
        }

        [Test]
        public void CollideMultipleCirclesAndRemoveItFromPool()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var collisionsFilter = world.Filter<Collision>().End();

            systems
               .Add(new FakeInitMultipleCircleCollidersSystem(new []
                {
                    (Vector2.zero, 0.5f),
                    (new Vector2(2, 0), 0.5f),
                    (new Vector2(0, -2), 0.5f)
                }))
               .Add(new FakeMoveMultipleObjectsSystem(new[]
                {
                    new Vector2(0, 0),
                    new Vector2(-1, 0),
                    new Vector2(0, 1)
                }))
               .Add(new CircleCollisionDetectionSystem())
               .Init();
            
            systems.Run();
            systems.Run();
            systems.Run();
            systems.Run();

            Assert.True(collisionsFilter.GetEntitiesCount() == 0);
        }
    }
}


