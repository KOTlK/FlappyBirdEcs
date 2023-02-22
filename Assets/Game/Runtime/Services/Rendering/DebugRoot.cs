using Game.Runtime.Services.Rendering.Physics;
using UnityEngine;

namespace Game.Runtime.Services.Rendering
{
    public class DebugRoot : MonoBehaviour
    {
        [field: SerializeField] public AABBDebugFactory AABB { get; private set; }
        [field: SerializeField] public ColliderDebugFactory CircleColliderFactory { get; private set; }
    }
}