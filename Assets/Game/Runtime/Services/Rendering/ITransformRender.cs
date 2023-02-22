using UnityEngine;

namespace Game.Runtime.Services.Rendering
{
    public interface ITransformRender
    {
        void DisplayInPosition(Vector2 position);
        void DisplayRotation(Quaternion rotation);
    }
}