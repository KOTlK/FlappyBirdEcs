using System;

namespace Game.Runtime.Services.Rendering.Pipes
{
    public interface IPipeRenderer : ITransform, IDisposable
    {
        void SetMaterial(PipeMaterial pipeMaterial);
        
        public enum PipeMaterial
        {
            UpperPipe,
            LowerPipe
        }
    }
}