using UnityEngine;

namespace Game.Runtime.Components.Physics
{
    public struct Rigidbody
    {
        public bool AffectedByPhysics { get; set; }
        public bool HasCustomGravity { get; set; }
        public Vector2 CustomGravity { get; set; }
        public Vector2 Velocity { get; set; }
        public float Speed => Velocity.magnitude;
    }
}