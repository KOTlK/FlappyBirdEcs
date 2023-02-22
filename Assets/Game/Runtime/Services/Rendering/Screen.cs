using UnityEngine;

namespace Game.Runtime.Services.Rendering
{
    public class Screen : IScreen
    {
        private readonly Camera _camera;

        public Screen(Camera camera)
        {
            _camera = camera;
        }
        
        public Vector2 Size => new(UnityEngine.Screen.width, UnityEngine.Screen.height);
        
        public Vector3 ScreenToWorld(Vector3 position) => _camera.ScreenToWorldPoint(position);
        public Vector3 ViewportToWorld(Vector3 position) => _camera.ViewportToWorldPoint(position);
        public Vector3 WorldToViewport(Vector3 position) => _camera.WorldToViewportPoint(position);
    }
}