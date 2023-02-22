using Game.Runtime.Startup;
using Leopotam.EcsLite;

namespace Game.Runtime.Systems.Time
{
    public class SyncTimeSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var time = systems.GetShared<Shared>().Time;
            time.DeltaTime = UnityEngine.Time.deltaTime;
            time.FixedDeltaTime = UnityEngine.Time.fixedDeltaTime;
            time.TimeSinceStartupInMilliseconds = (long)(UnityEngine.Time.realtimeSinceStartupAsDouble * 1000);
        }
    }
}