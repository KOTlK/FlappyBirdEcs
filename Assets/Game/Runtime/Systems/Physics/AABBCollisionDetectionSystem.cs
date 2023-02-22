using Game.Runtime.Components.Physics;
using Game.Runtime.Utils;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Physics
{
    public class AABBCollisionDetectionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var transformPool = world.GetPool<Transform>();
            var collisionPool = world.GetPool<Collision>();
            var aabbPool = world.GetPool<AABB>();
            var filter = world.Filter<AABB>().Inc<Rigidbody>().Inc<Transform>().End();
            var collisionsFilter = world.Filter<Collision>().End();

            foreach (var firstEntity in filter)
            {
                foreach (var secondEntity in filter)
                {
                    if (firstEntity == secondEntity)
                        continue;

                    ref var firstTransform = ref transformPool.Get(firstEntity);
                    ref var secondTransform = ref transformPool.Get(secondEntity);
                    ref var firstAABB = ref aabbPool.Get(firstEntity);
                    ref var secondAABB = ref aabbPool.Get(secondEntity);

                    var collided = PhysicsUtils.Intersect((firstAABB, firstTransform), (secondAABB, secondTransform));

                    if (collided)
                    {
                        if (PhysicsUtils.HasCollision(collisionsFilter, collisionPool, firstEntity, secondEntity))
                            continue;

                        var collisionEntity = world.NewEntity();

                        ref var collision = ref collisionPool.Add(collisionEntity);

                        collision.FirstEntity = firstEntity;
                        collision.SecondEntity = secondEntity;
                    }
                    else
                    { 
                        if (PhysicsUtils.HasCollision(collisionsFilter, collisionPool, firstEntity, secondEntity,
                                                    out var collidedEntity))
                        {
                            collisionPool.Del(collidedEntity);
                        }
                    }
                }
            }
        }

    }
}