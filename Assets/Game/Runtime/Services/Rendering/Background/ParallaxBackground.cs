using UnityEngine;

namespace Game.Runtime.Services.Rendering.Background
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ParallaxBackground : MonoBehaviour, IBackground
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private Material _material;
        private static readonly int _speed = Shader.PropertyToID("_Speed");

        public void Create(Vector2 size)
        {
            var halfExtents = size / 2;
            
            var vertices = new Vector3[]
            {
                new(-halfExtents.x, -halfExtents.y, 0),
                new(halfExtents.x, -halfExtents.y, 0),
                new(-halfExtents.x, halfExtents.y, 0),
                new(halfExtents.x, halfExtents.y, 0),
            };
            var uvs = new Vector2[]
            {
                new (0, 0),
                new (1, 0),
                new (0, 1),
                new (1, 1)
            };
            var normals = new Vector3[]
            {
                Vector3.back,
                Vector3.back,
                Vector3.back,
                Vector3.back,
            };
            var triangles = new int[]
            {
                0, 2, 1,
                2, 3, 1
            };

            var mesh = new Mesh
            {
                vertices = vertices,
                triangles = triangles,
                normals = normals,
                uv = uvs,
            };

            _meshFilter.mesh = mesh;
            _meshRenderer.material = _material;
        }

        public void StartAnimation(float speed = 0.1f)
        {
            _meshRenderer.material.SetFloat(_speed, speed);
        }


        public void StopAnimation()
        {
            _meshRenderer.material.SetFloat(_speed, 0f);
        }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public Quaternion Rotation { get; set; }
    }
}