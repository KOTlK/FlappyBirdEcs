using UnityEngine;

namespace Game.Runtime.Services.Rendering.Pipes
{
    public interface IPipeFactory
    {
        PipeRenderer Create(Vector2 size);
    }
}