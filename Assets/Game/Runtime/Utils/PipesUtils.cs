using System;
using Game.Runtime.Services.Difficulty;
using Game.Runtime.Services.Rendering;
using Game.Runtime.Services.Rendering.Pipes;
using UnityEngine;

namespace Game.Runtime.Systems.Pipes
{
    public static class PipesUtils
    {
        public static IPipeRenderer GeneratePipe(IPipeFactory factory, IScreen screen, Difficulty difficulty, out Vector2 totalSize, bool upper = true)
        {
            var worldPosition = GetWorldPositionAndSize(upper, screen, difficulty, out var size);

            var pipe = factory.Create(size);
            pipe.Position = worldPosition;

            totalSize = size;
            return pipe;
        }

        public static Vector2 GetWorldPositionAndSize(bool upper, IScreen screen, Difficulty difficulty, out Vector2 totalSize)
        {
            var viewportStartPosition = upper switch
            {
                true => new Vector2(1, 1),
                false => new Vector2(1, 0)
            };

            var viewportEndPosition = upper switch
            {
                true => new Vector2(1 - difficulty.SoloPipeWidth, 1 - difficulty.SoloPipeHeight),
                false => new Vector2(1 - difficulty.SoloPipeWidth, difficulty.SoloPipeHeight)
            };
            
            var viewportCenter = (viewportEndPosition - viewportStartPosition) / 2;
            viewportCenter.x += difficulty.SoloPipeWidth;
            var worldStartPosition = screen.ViewportToWorld(viewportStartPosition);
            var worldEndPosition = screen.ViewportToWorld(viewportEndPosition);

            var worldPosition = screen.ViewportToWorld(viewportStartPosition + viewportCenter);
            var size = new Vector2(Math.Abs(worldStartPosition.x - worldEndPosition.x),
                                   Math.Abs(worldStartPosition.y - worldEndPosition.y));

            totalSize = size;
            return worldPosition;
        }
        
        public static Vector2 GetWorldPosition(bool upper, IScreen screen, Difficulty difficulty)
        {
            var viewportStartPosition = upper switch
            {
                true => new Vector2(1, 1),
                false => new Vector2(1, 0)
            };

            var viewportEndPosition = upper switch
            {
                true => new Vector2(1 - difficulty.SoloPipeWidth, 1 - difficulty.SoloPipeHeight),
                false => new Vector2(1 - difficulty.SoloPipeWidth, difficulty.SoloPipeHeight)
            };
            
            var viewportCenter = (viewportEndPosition - viewportStartPosition) / 2;
            viewportCenter.x += difficulty.SoloPipeWidth;

            var worldPosition = screen.ViewportToWorld(viewportStartPosition + viewportCenter);
            return worldPosition;
        }

        public static Vector2 GetScorePositionAndSize(IScreen screen, Difficulty difficulty, out Vector2 size)
        {
            var viewportStartPosition = new Vector2(1 + difficulty.SoloPipeWidth, difficulty.SoloPipeHeight);
            var viewportEndPosition = new Vector2(1, 1 - difficulty.SoloPipeHeight);
            var viewportCenter = (viewportEndPosition - viewportStartPosition) / 2;

            var worldStartPosition = screen.ViewportToWorld(viewportStartPosition);
            var worldEndPosition = screen.ViewportToWorld(viewportEndPosition);

            var worldPosition = screen.ViewportToWorld(viewportStartPosition + viewportCenter);
            var totalSize = new Vector2(Math.Abs(worldStartPosition.x - worldEndPosition.x),
                                   Math.Abs(worldStartPosition.y - worldEndPosition.y));

            size = totalSize;
            return worldPosition;
        }

        public static Vector2 GetScorePosition(IScreen screen, Difficulty difficulty)
        {
            var viewportStartPosition = new Vector2(1 + difficulty.SoloPipeWidth, difficulty.SoloPipeHeight);
            var viewportEndPosition = new Vector2(1, 1 - difficulty.SoloPipeHeight);
            var viewportCenter = (viewportEndPosition - viewportStartPosition) / 2;

            var worldPosition = screen.ViewportToWorld(viewportStartPosition + viewportCenter);
            return worldPosition;
        }
    }
}