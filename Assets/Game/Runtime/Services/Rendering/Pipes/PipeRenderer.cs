using System;
using UnityEngine;

namespace Game.Runtime.Services.Rendering.Pipes
{
    [RequireComponent(typeof(MeshFilter))]
    public class PipeRenderer : MonoBehaviour, IPipeRenderer
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _upper;
        [SerializeField] private Material _lower;
        [field: SerializeField] public MeshFilter MeshFilter { get; private set; }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public void SetMaterial(IPipeRenderer.PipeMaterial pipeMaterial)
        {
            _renderer.material = pipeMaterial switch
            {
                IPipeRenderer.PipeMaterial.UpperPipe => _upper,
                IPipeRenderer.PipeMaterial.LowerPipe => _lower,
                _ => throw new ArgumentOutOfRangeException(nameof(pipeMaterial), pipeMaterial, null)
            };
        }
    }
}