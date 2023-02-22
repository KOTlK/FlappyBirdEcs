using UnityEngine;

namespace Game.Runtime.Services.Rendering
{
    public interface ITransform
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}