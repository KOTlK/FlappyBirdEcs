using UnityEngine;

namespace Game.Runtime.Services.Rendering
{
    public interface IScreen
    {
        Vector2 Size { get; }
        Vector3 ScreenToWorld(Vector3 position);
        Vector3 ViewportToWorld(Vector3 position);
        Vector3 WorldToViewport(Vector3 position);
    }
}