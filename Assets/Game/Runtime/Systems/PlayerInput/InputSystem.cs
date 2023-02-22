using Game.Runtime.Components.Events.Commands;
using Game.Runtime.Components.Pipes.Events.Input;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Runtime.Systems.Events.PlayerInput
{
    public class InputSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<ScreenTapEvent>();
            var spawnPipeCommandsPool = world.GetPool<SpawnPipesCommand>();
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var entity = world.NewEntity();
                
                pool.Add(entity);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var entity = world.NewEntity();
                spawnPipeCommandsPool.Add(entity);
            }
        }
    }
}