using UnityEngine;

namespace Game.Runtime.Services.Rendering.Physics
{
    [RequireComponent(typeof(LineRenderer))]
    public class AABBDebug : MonoBehaviour, IAABBDebugRender
    {
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetVertices(Vector2[] vertices)
        {
            _lineRenderer.positionCount = vertices.Length;

            for (var i = 0; i < vertices.Length; ++i)
            {
                _lineRenderer.SetPosition(i, vertices[i]);
            }
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}