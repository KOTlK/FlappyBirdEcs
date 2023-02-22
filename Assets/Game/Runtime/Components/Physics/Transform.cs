using UnityEngine;

namespace Game.Runtime.Components.Physics
{
    public struct Transform
    {
        public Transform(Vector2 position)
        {
            Position = position;
        }
        public Vector2 Position { get; set; }
    }
}