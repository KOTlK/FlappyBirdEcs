using Game.Runtime.Components.Physics;
using Game.Runtime.Components.Physics.Debug;
using Game.Runtime.Startup;
using Leopotam.EcsLite;
using UnityEngine;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Runtime.Systems.Rendering
{
    public class AABBRenderingSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var factory = systems.GetShared<Shared>().DebugAABB;
            var renderersPool = world.GetPool<AABBRendering>();
            var aabbPool = world.GetPool<AABB>();
            var transformPool = world.GetPool<Transform>();
            var filter = world.Filter<AABB>().Inc<Transform>().End();
            var existingRenderers = world.Filter<AABB>().Inc<AABBRendering>().Inc<Transform>().End();

            foreach (var entity in existingRenderers)
            {
                ref var transform = ref transformPool.Get(entity);
                ref var aabb = ref aabbPool.Get(entity);
                ref var renderer = ref renderersPool.Get(entity);
                
                var halfExtents = aabb.HalfExtents;
                var leftDown = transform.Position + new Vector2(-halfExtents.x, -halfExtents.y);
                var rightDown = transform.Position + new Vector2(halfExtents.x, -halfExtents.y);
                var rightUp = transform.Position + new Vector2(halfExtents.x, halfExtents.y);
                var leftUp = transform.Position + new Vector2(-halfExtents.x, halfExtents.y);

                renderer.Renderer.SetVertices(new []
                {
                    leftDown,
                    rightDown,
                    rightUp,
                    leftUp,
                    leftDown
                });
                renderer.Renderer.SetPosition(transform.Position);
            }

            foreach (var aabbEntity in filter)
            {
                if (renderersPool.Has(aabbEntity))
                    continue;

                ref var transform = ref transformPool.Get(aabbEntity);
                ref var aabb = ref aabbPool.Get(aabbEntity);
                ref var renderer = ref renderersPool.Add(aabbEntity);

                var halfExtents = aabb.HalfExtents;
                var leftDown = transform.Position + new Vector2(-halfExtents.x, -halfExtents.y);
                var rightDown = transform.Position + new Vector2(halfExtents.x, -halfExtents.y);
                var rightUp = transform.Position + new Vector2(halfExtents.x, halfExtents.y);
                var leftUp = transform.Position + new Vector2(-halfExtents.x, halfExtents.y);

                var spawnedRenderer = factory.Create(new[]
                {
                    leftDown,
                    rightDown,
                    rightUp,
                    leftUp,
                    leftDown
                });

                renderer.Renderer = spawnedRenderer;
                renderer.Renderer.SetPosition(transform.Position);
            }
        }
    }
}