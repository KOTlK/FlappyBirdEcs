using Game.Runtime.Components.Characters;
using Game.Runtime.Components.Events.Commands;
using Game.Runtime.Components.Pipes;
using Game.Runtime.Startup;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Session
{
    public class RestartSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var ui = systems.GetShared<Shared>().UserInterface;
            var score = systems.GetShared<Shared>().Score;
            var background = systems.GetShared<Shared>().Background;
            var birdsPool = world.GetPool<Bird>();
            var commandsPool = world.GetPool<InitializeSessionCommand>();
            var aliveBirdsFilter = world.Filter<Bird>().Exc<Dead>().End();
            var deadBirdsFilter = world.Filter<Bird>().Inc<Dead>().End();

            if (aliveBirdsFilter.GetEntitiesCount() == 0)
            {
                if (ui.MainMenu.Active)
                    return;
                
                if (ui.LoseScreen.Active == false)
                {
                    ui.MainMenu.Active = false;
                    ui.InGame.Active = false;
                    ui.LoseScreen.Active = true;
                    background.StopAnimation();
                }

                ref var bird = ref birdsPool.Get(deadBirdsFilter.GetRawEntities()[0]);
                var currentScore = bird.Score;

                ui.LoseScreen.CurrentScore.SetString(currentScore.ToString());
                if (score.High < currentScore)
                {
                    score.High = currentScore;
                }

                ui.LoseScreen.HighScore.SetString(score.High.ToString());
            }
            
            if (ui.LoseScreen.Active)
            {
                if (ui.LoseScreen.Restart.Clicked)
                {
                    ui.InGame.Score.SetString(0.ToString());
                    commandsPool.Add(world.NewEntity());
                    ui.LoseScreen.Restart.Reset();
                }
            }
        }
    }
}