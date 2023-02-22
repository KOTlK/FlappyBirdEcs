using System;
using Game.Runtime.Startup;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Runtime.Systems.Rendering
{
    public class GenerateBackgroundSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var background = systems.GetShared<Shared>().Background;
            var screen = systems.GetShared<Shared>().Screen;

            var leftDown = new Vector3(-4, 0);
            var rightDown = new Vector3(4, 0);
            var rightUp = new Vector3(4, 1);
            var leftUp = new Vector3(-4, 1);

            var leftDownWorld = screen.ViewportToWorld(leftDown);
            var rightDownWorld = screen.ViewportToWorld(rightDown);
            var rightUpWorld = screen.ViewportToWorld(rightUp);
            var leftUpWorld = screen.ViewportToWorld(leftUp);

            var size = new Vector2(Math.Abs((rightUpWorld - leftUpWorld).magnitude),
                                   Math.Abs((rightUpWorld - rightDownWorld).magnitude));

            var position = leftDownWorld + (rightUpWorld - leftDownWorld) / 2;
            position.z = 1f;

            background.Create(size);
            background.Position = position;
            background.StopAnimation();
        }
    }
}