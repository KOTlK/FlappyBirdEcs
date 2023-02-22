using System;
using UnityEngine;

namespace Game.Runtime.Startup.Config.Physics
{
    [Serializable]
    public class PhysicsConfig
    {
        [field: SerializeField] public Vector2 Gravity { get; set; }
    }
}