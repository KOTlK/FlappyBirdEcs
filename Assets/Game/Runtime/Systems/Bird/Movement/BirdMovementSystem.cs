using Game.Runtime.Components.Pipes;
using Game.Runtime.Components.Pipes.Events.Input;
using Game.Runtime.Startup;
using Leopotam.EcsLite;
using UnityEngine;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;

namespace Game.Runtime.Systems.Events.Bird.Movement
{
    public class BirdMovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var difficulty = systems.GetShared<Shared>().StaticData.Difficulty;
            var tapEvents = world.Filter<ScreenTapEvent>().End();
            var rigidbodiesPool = world.GetPool<Rigidbody>();
            var playerFilter = world.Filter<Rigidbody>().Inc<ControlledByPlayer>().Inc<Components.Characters.Bird>().Exc<Dead>().End();
            
            foreach (var entity in playerFilter)
            {
                ref var rigidbody = ref rigidbodiesPool.Get(entity);
                var velocity = rigidbody.Velocity;
            
                foreach (var tapEvent in tapEvents)
                {
                    velocity.y += difficulty.BirdJumpForce;
                }

                velocity.y = Mathf.Clamp(velocity.y, -5, 5);
                rigidbody.Velocity = velocity;
            }
            
        }
    }
}