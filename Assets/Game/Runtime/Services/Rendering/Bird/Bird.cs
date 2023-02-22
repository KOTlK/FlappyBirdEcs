using UnityEngine;

namespace Game.Runtime.Services.Rendering.Bird
{
    public class Bird : MonoBehaviour, IBirdRenderer
    {
        [SerializeField] private Animator _animator;

        private readonly int _velocity = Animator.StringToHash("Velocity");

        public void Dispose()
        {
            Destroy(gameObject);
        }
        
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

        public float VelocityY
        {
            set => _animator.SetFloat(_velocity, value);
        }
    }
}