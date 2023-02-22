namespace Game.Runtime.Services
{
    public class Time
    {
        private float _deltaTime;
        private float _fixedDeltaTime;

        public float TimeScale { get; set; } = 1f;

        public float DeltaTime
        {
            get => _deltaTime * TimeScale;
            set => _deltaTime = value;
        }

        public float FixedDeltaTime
        {
            get => _fixedDeltaTime * TimeScale;
            set => _fixedDeltaTime = value;
        }
        public long TimeSinceStartupInMilliseconds { get; set; }
    }
}