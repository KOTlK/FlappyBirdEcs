using System;

namespace Game.Runtime.Services.Rendering.Bird
{
    public interface IBirdRenderer : ITransform, IDisposable
    {
        float VelocityY { set; }
    }
}