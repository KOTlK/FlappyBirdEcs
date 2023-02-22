using Game.Runtime.Components.Characters;
using Game.Runtime.Components.Events.Commands;
using Game.Runtime.Components.Physics;
using Game.Runtime.Components.Pipes;
using Game.Runtime.Components.Pipes.Events.Input;
using Game.Runtime.Startup;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Session
{
    public class StartGameSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var shared = systems.GetShared<Shared>();
            var ui = shared.UserInterface;
            var background = shared.Background;
            var settings = shared.StaticData;
            var rigidbodiesPool = world.GetPool<Rigidbody>();
            var spawnCommandsPool = world.GetPool<SpawnPipesCommand>();
            var birdsFilter = world.Filter<Bird>().Exc<Dead>().End();
            var clicksFilter = world.Filter<ScreenTapEvent>().End();
            var pipesFilter = world.Filter<Pipe>().End();

            if (birdsFilter.GetEntitiesCount() == 0)
                return;

            if (pipesFilter.GetEntitiesCount() != 0)
                return;

            foreach (var click in clicksFilter)
            {
                foreach (var birdEntity in birdsFilter)
                {
                    ref var rigidbody = ref rigidbodiesPool.Get(birdEntity);
                    rigidbody.AffectedByPhysics = true;
                }

                ui.MainMenu.Active = false;
                ui.InGame.Active = true;
                background.StartAnimation(settings.BackgroundSpeed);

                spawnCommandsPool.Add(world.NewEntity());
            }
        }
    }
}