using System;
using UnityEngine;

namespace Game.Runtime.Services.Rendering.Physics
{
    public interface ICircleColliderDebugRenderer : IDisposable
    {
        void SetRadius(float radius);
        void SetPosition(Vector2 position);
    }
}