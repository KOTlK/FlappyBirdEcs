using Game.Runtime.Components.Physics;
using Game.Runtime.Utils;
using Leopotam.EcsLite;
using Collision = Game.Runtime.Components.Physics.Collision;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Runtime.Systems.Physics
{
    public class CircleCollisionDetectionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var circlesPool = world.GetPool<CircleCollider>();
            var transformsPool = world.GetPool<Transform>();
            var collisionsPool = world.GetPool<Collision>();
            var filter = world.Filter<CircleCollider>().Inc<Rigidbody>().Inc<Transform>().End();
            var collisionsFilter = world.Filter<Collision>().End();

            foreach (var firstEntity in filter)
            {
                foreach (var secondEntity in filter)
                {
                    if (firstEntity == secondEntity)
                        continue;

                    ref var transform1 = ref transformsPool.Get(firstEntity);
                    ref var transform2 = ref transformsPool.Get(secondEntity);
                    ref var collider1 = ref circlesPool.Get(firstEntity);
                    ref var collider2 = ref circlesPool.Get(secondEntity);

                    var collided = PhysicsUtils.Intersect((transform1.Position, collider1.Radius),
                                            (transform2.Position, collider2.Radius));

                    if (collided)
                    {
                        if (PhysicsUtils.HasCollision(collisionsFilter, collisionsPool, firstEntity, secondEntity))
                            continue;
                        
                        var collisionEntity = world.NewEntity();
                        
                        ref var collision1 = ref collisionsPool.Add(collisionEntity);

                        collision1.FirstEntity = firstEntity;
                        collision1.SecondEntity = secondEntity;
                    }
                    else
                    {
                        if(PhysicsUtils.HasCollision(collisionsFilter, collisionsPool, firstEntity, secondEntity, out var collidedEntity))
                        {
                            collisionsPool.Del(collidedEntity);
                        }
                    }
                }
            }
        }
    }
}