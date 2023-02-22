using Game.Runtime.Components.Physics;
using Game.Runtime.Utils;
using Leopotam.EcsLite;
using Collision = Game.Runtime.Components.Physics.Collision;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Runtime.Systems.Physics
{
    public class AABBCircleCollisionDetectionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var collisionsPool = world.GetPool<Collision>();
            var transformsPool = world.GetPool<Transform>();
            var circlesPool = world.GetPool<CircleCollider>();
            var aabbPool = world.GetPool<AABB>();
            var triggersPool = world.GetPool<TriggerCollision>();
            var circlesFilter = world.Filter<Transform>().Inc<CircleCollider>().End();
            var aabbFilter = world.Filter<Transform>().Inc<AABB>().End();
            var collisionsFilter = world.Filter<Collision>().End();
            var triggerCollisionsFilter = world.Filter<TriggerCollision>().End();

            foreach (var circleEntity in circlesFilter)
            {
                foreach (var aabbEntity in aabbFilter)
                {
                    ref var circleTransform = ref transformsPool.Get(circleEntity);
                    ref var aabbTransform = ref transformsPool.Get(aabbEntity);
                    ref var circleCollider = ref circlesPool.Get(circleEntity);
                    ref var aabbCollider = ref aabbPool.Get(aabbEntity);

                    var aabb = (aabbCollider, aabbTransform);
                    var circle = (circleCollider, circleTransform);

                    if (PhysicsUtils.Intersect(circle, aabb))
                    {
                        if (PhysicsUtils.HasCollision(collisionsFilter, collisionsPool, circleEntity, aabbEntity))
                            continue;

                        if (PhysicsUtils.HasTriggerCollision(triggerCollisionsFilter, triggersPool, circleEntity,
                                                             aabbEntity, out var trigger))
                        {
                            ref var triggerCollision = ref triggersPool.Get(trigger);

                            triggerCollision.ExecutedThisFrame = false;
                            continue;
                        }

                        if (aabbCollider.IsTrigger || circleCollider.IsTrigger)
                        {
                            var triggerEntity = world.NewEntity();

                            ref var newTrigger = ref triggersPool.Add(triggerEntity);

                            newTrigger.FirstEntity = circleEntity;
                            newTrigger.SecondEntity = aabbEntity;
                            newTrigger.ExecutedThisFrame = true;
                            
                            continue;
                        }

                        var collisionEntity = world.NewEntity();

                        ref var collision = ref collisionsPool.Add(collisionEntity);

                        collision.FirstEntity = circleEntity;
                        collision.SecondEntity = aabbEntity;
                    }
                    else
                    {
                        if (PhysicsUtils.HasCollision(collisionsFilter, collisionsPool, circleEntity, aabbEntity, out var entityForRemove))
                        {
                            collisionsPool.Del(entityForRemove);
                        }
                        
                        if (PhysicsUtils.HasTriggerCollision(triggerCollisionsFilter, triggersPool, circleEntity,
                                                             aabbEntity, out var trigger))
                        {

                            triggersPool.Del(trigger);
                        }
                    }
                }
            }
        }
    }
}