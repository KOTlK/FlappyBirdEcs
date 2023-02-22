using Game.Runtime.Components.Physics;
using Game.Runtime.Utils;
using NUnit.Framework;
using UnityEngine;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Tests.Utils
{
    public class PhysicsUtilsTests
    {
        [Test]
        public void CheckIntersectionBetweenAABBAndCircle()
        {
            var aabb = (new AABB {Size = new Vector2(1, 1)}, new Transform {Position = Vector2.zero});
            var circle = (new CircleCollider {Radius = 0.5f}, new Transform {Position = new Vector2(0, -1)});

            Assert.True(PhysicsUtils.Intersect(circle, aabb));
        }
    }
}