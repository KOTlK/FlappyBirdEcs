using UnityEngine;

namespace Game.Runtime.Services.Rendering.Pipes
{
    public class PipesFactory : MonoBehaviour, IPipeFactory
    {
        [SerializeField] private PipeRenderer _pipePrefab;
        
        public PipeRenderer Create(Vector2 size)
        {
            var pipe = Instantiate(_pipePrefab);
            
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
                uv = uvs,
                triangles = triangles,
                normals = normals,
            };
            
            pipe.MeshFilter.mesh = mesh;

            return pipe;
        }
    }
}