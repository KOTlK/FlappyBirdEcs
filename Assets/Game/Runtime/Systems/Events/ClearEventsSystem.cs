using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Events
{
    public class ClearEventsSystem<TEvent> : IEcsRunSystem 
        where TEvent : struct
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var events = world.GetPool<TEvent>();
            var entities = world.Filter<TEvent>().End();
            
            foreach (var entity in entities)
            {
                events.Del(entity);
            }
        }
    }
}