using Game.Runtime.Components.Physics;
using Leopotam.EcsLite;
using UnityEngine;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Tests.Systems.Physics
{
    public class FakeMoveMultipleObjectsSystem : IEcsRunSystem
    {
        private readonly Vector2[] _movements;

        public FakeMoveMultipleObjectsSystem(Vector2[] movements)
        {
            _movements = movements;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var transformPool = world.GetPool<Transform>();
            var filter = world.Filter<Transform>().Inc<Rigidbody>().End();
            var index = 0;

            foreach (var entity in filter)
            {
                ref var transform = ref transformPool.Get(entity);

                transform.Position += _movements[index];
                index++;
            }
        }
    }
}