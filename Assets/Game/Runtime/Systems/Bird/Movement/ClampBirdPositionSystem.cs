using Game.Runtime.Startup;
using Leopotam.EcsLite;
using UnityEngine;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Runtime.Systems.Events.Bird.Movement
{
    public class ClampBirdPositionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var screen = systems.GetShared<Shared>().Screen;
            var transformsPool = world.GetPool<Transform>();
            var filter = world.Filter<Components.Characters.Bird>().Inc<Transform>().End();

            var maxWorldPosition = screen.ViewportToWorld(new Vector3(0, 1, 0));
            var minWorldPosition = screen.ViewportToWorld(new Vector3(0, 0, 0));

            foreach (var entity in filter)
            {
                ref var transform = ref transformsPool.Get(entity);

                var y = Mathf.Clamp(transform.Position.y, minWorldPosition.y, maxWorldPosition.y);

                transform.Position = new Vector2(transform.Position.x, y);
            }
        }
    }
}