using UnityEngine;

namespace Game.Runtime.Services.Rendering.Physics
{
    [RequireComponent(typeof(LineRenderer))]
    public class CircleCollider : MonoBehaviour, ICircleColliderDebugRenderer
    {
        [SerializeField] private int _linesCount = 20;
        
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            
        }
        
        
        public void SetRadius(float radius)
        {
            var angle = 360 / _linesCount;
            var vector = Vector2.up * radius;
            _lineRenderer.positionCount = _linesCount;
            _lineRenderer.SetPosition(0, vector);

            for (var i = 1; i < _linesCount; ++i)
            {
                var cos = Mathf.Cos(Mathf.Deg2Rad * angle);
                var sin = Mathf.Sin(Mathf.Deg2Rad * angle);
                var x = vector.x * cos - vector.y * sin;
                var y = vector.x * sin + vector.y * cos;

                vector = new Vector2(x, y);
                _lineRenderer.SetPosition(i, vector);
            }

            _lineRenderer.SetPosition(_linesCount - 1, Vector2.up * radius);
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