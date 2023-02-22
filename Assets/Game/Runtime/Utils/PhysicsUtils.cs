using System;
using Game.Runtime.Components.Physics;
using Leopotam.EcsLite;
using UnityEngine;
using Collision = Game.Runtime.Components.Physics.Collision;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Runtime.Utils
{
    public static class PhysicsUtils
    {
        public static bool HasTriggerCollision(EcsFilter filter, EcsPool<TriggerCollision> pool, int first, int second)
        {
            foreach (var entity in filter)
            {
                ref var trigger = ref pool.Get(entity);

                if (trigger.Has(first) && trigger.Has(second))
                {
                    return true;
                }
            }

            return false;
        }
        
        public static bool HasTriggerCollision(EcsFilter filter, EcsPool<TriggerCollision> pool, int first, int second, out int collision)
        {
            foreach (var entity in filter)
            {
                ref var trigger = ref pool.Get(entity);

                if (trigger.Has(first) && trigger.Has(second))
                {
                    collision = entity;
                    return true;
                }
            }

            collision = -1;
            return false;
        }
        
        public static bool HasCollision(EcsFilter filter, EcsPool<Collision> pool, int first, int second)
        {
            foreach (var entity in filter)
            {
                ref var collision = ref pool.Get(entity);

                if (collision.Has(first) && collision.Has(second))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasCollision(EcsFilter filter, EcsPool<Collision> pool, int first, int second, out int collidedEntity)
        {
            foreach (var entity in filter)
            {
                ref var collision = ref pool.Get(entity);

                if (collision.Has(first) && collision.Has(second))
                {
                    collidedEntity = entity;
                    return true;
                }
            }

            collidedEntity = -1;
            return false;
        }
        
        public static bool Intersect((Vector2 position, float radius) first, (Vector2 position, float radius) second)
        {
            var dx = first.position.x - second.position.x;
            var dy = first.position.y - second.position.y;
            var distance = Math.Sqrt(dx * dx + dy * dy);

            return distance <= first.radius + second.radius;
        }
        
        public static bool Intersect((AABB aabb, Transform transform) first, (AABB aabb, Transform transform) second)
        {
            return first.transform.Position.x + first.aabb.Size.x >= second.transform.Position.x &&
                   second.transform.Position.x + second.aabb.Size.x >= first.transform.Position.x &&
                   first.transform.Position.y + first.aabb.Size.y >= second.transform.Position.y &&
                   second.transform.Position.y + second.aabb.Size.y >= first.transform.Position.y;
        }

        public static bool Intersect((CircleCollider circle, Transform transform) circle,
                                     (AABB aabb, Transform transform) aabb)
        {
            var difference = circle.transform.Position - aabb.transform.Position;
            var halfExtents = aabb.aabb.HalfExtents;
            var clamped = Clamp(difference, -halfExtents, halfExtents);
            var closest = aabb.transform.Position + clamped;
            difference = closest - circle.transform.Position;

            return difference.sqrMagnitude <= circle.circle.Radius * circle.circle.Radius;
        }

        private static Vector2 Clamp(Vector2 vector, Vector2 min, Vector2 max)
        {
            return new Vector2(Mathf.Clamp(vector.x, min.x, max.x), Mathf.Clamp(vector.y, min.y, max.y));
        }
    }
}