using UnityEngine;

namespace Game.Runtime.Services.Rendering.Physics
{
    public class AABBDebugFactory : MonoBehaviour
    {
        [SerializeField] private AABBDebug _prefab;

        public IAABBDebugRender Create(Vector2[] vertices)
        {
            var origin = Instantiate(_prefab);
            origin.SetVertices(vertices);

            return origin;
        }
    }
}