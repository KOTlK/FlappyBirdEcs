using System;
using UnityEngine;

namespace Game.Runtime.Services.Rendering.Physics
{
    public interface IAABBDebugRender : IDisposable
    {
        void SetVertices(Vector2[] vertices);
        void SetPosition(Vector2 position);
    }
}