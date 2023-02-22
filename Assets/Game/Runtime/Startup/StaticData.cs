using Game.Runtime.Services.Difficulty;
using Game.Runtime.Startup.Config.Physics;
using UnityEngine;

namespace Game.Runtime.Startup
{
    [CreateAssetMenu(menuName = "New/StaticData")]
    public class StaticData : ScriptableObject
    {
        [field: SerializeField] public PhysicsConfig PhysicsConfig { get; private set; }
        [field: SerializeField] public Services.Rendering.Bird.Bird BirdPrefab { get; private set; }
        [field: SerializeField] public float BirdColliderRadius { get; private set; }
        [field: SerializeField] public float BackgroundSpeed { get; private set; }
        [field: SerializeField] public Difficulty Difficulty { get; private set; }
    }
}