using UnityEngine;

namespace Game.Runtime.Services.Rendering.Background
{
    public interface IBackground : ITransform
    {
        void Create(Vector2 size);
        void StartAnimation(float speed = 1f);
        void StopAnimation();
    }
}