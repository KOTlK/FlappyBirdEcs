using UnityEngine;

namespace Game.Runtime.Components.Physics
{
    public struct AABB
    {
        public bool IsTrigger { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 HalfExtents => Size / 2;
    }
}