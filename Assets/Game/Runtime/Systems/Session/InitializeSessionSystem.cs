using Game.Runtime.Components.Characters;
using Game.Runtime.Components.Events.Commands;
using Game.Runtime.Components.Physics;
using Game.Runtime.Components.Physics.Debug;
using Game.Runtime.Components.Pipes;
using Game.Runtime.Startup;
using Leopotam.EcsLite;
using UnityEngine;
using Collision = Game.Runtime.Components.Physics.Collision;
using Rigidbody = Game.Runtime.Components.Physics.Rigidbody;
using Transform = Game.Runtime.Components.Physics.Transform;

namespace Game.Runtime.Systems.Session
{
    public class InitializeSessionSystem : IEcsRunSystem, IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var ui = systems.GetShared<Shared>().UserInterface;
            var birdsPool = world.GetPool<Bird>();
            var shared = systems.GetShared<Shared>();
            var controlledByPlayer = world.GetPool<ControlledByPlayer>();
            var colliderPool = world.GetPool<CircleCollider>();
            var transformPool = world.GetPool<Transform>();
            var staticData = shared.StaticData;
            var rigidbodiesPool = world.GetPool<Rigidbody>();
            
            var birdEntity = world.NewEntity();
            var obj = Object.Instantiate(staticData.BirdPrefab, Vector3.zero, Quaternion.identity);
            ref var collider = ref colliderPool.Add(birdEntity);
            ref var transform = ref transformPool.Add(birdEntity);
            ref var bird = ref birdsPool.Add(birdEntity);
            ref var rigidbody = ref rigidbodiesPool.Add(birdEntity);
            controlledByPlayer.Add(birdEntity);

            rigidbody.AffectedByPhysics = false;
            rigidbody.HasCustomGravity = true;
            rigidbody.CustomGravity = new Vector2(0, -shared.StaticData.Difficulty.BirdFallSpeed);

            bird.Renderer = obj;
            collider.Radius = shared.StaticData.BirdColliderRadius;
            transform.Position = new Vector2(-2f, 0);

            ui.LoseScreen.Active = false;
            ui.InGame.Active = false;
            ui.MainMenu.Active = true;
            
            ui.InGame.Score.SetString(0.ToString());
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var ui = systems.GetShared<Shared>().UserInterface;
            var pipesPool = world.GetPool<Pipe>();
            var birdsPool = world.GetPool<Bird>();
            var shared = systems.GetShared<Shared>();
            var controlledByPlayer = world.GetPool<ControlledByPlayer>();
            var colliderPool = world.GetPool<CircleCollider>();
            var transformPool = world.GetPool<Transform>();
            var staticData = shared.StaticData;
            var rigidbodiesPool = world.GetPool<Rigidbody>();
            var commandsPool = world.GetPool<InitializeSessionCommand>();
            var aabbRenderersPool = world.GetPool<AABBRendering>();
            var circleRenderersPool = world.GetPool<CircleRendering>();
            var birdFilter = world.Filter<Bird>().End();
            var pipesFilter = world.Filter<Pipe>().End();
            var collisionsFilter = world.Filter<Collision>().End();
            var aabbsFilter = world.Filter<AABB>().End();
            var circleCollidersFilter = world.Filter<CircleCollider>().End();
            var pipeGroupsFilter = world.Filter<PipeGroup>().End();
            var triggersFilter = world.Filter<TriggerCollision>().End();
            var debugAABBFilter = world.Filter<AABBRendering>().End();
            var debugCircleFilter = world.Filter<CircleRendering>().End();
            var commands = world.Filter<InitializeSessionCommand>().End();

            foreach (var command in commands)
            {
                foreach (var entity in birdFilter)
                {
                    ref var existingBird = ref birdsPool.Get(entity);
                    existingBird.Renderer.Dispose();
                    world.DelEntity(entity);
                }

                foreach (var entity in pipesFilter)
                {
                    ref var pipe = ref pipesPool.Get(entity);
                    pipe.Renderer.Dispose();
                    world.DelEntity(entity);
                }

                foreach (var entity in collisionsFilter)
                {
                    world.DelEntity(entity);
                }

                foreach (var entity in debugAABBFilter)
                {
                    ref var renderer = ref aabbRenderersPool.Get(entity);
                    renderer.Renderer.Dispose();
                    aabbRenderersPool.Del(entity);
                }
                
                foreach (var entity in debugCircleFilter)
                {
                    ref var renderer = ref circleRenderersPool.Get(entity);
                    renderer.Renderer.Dispose();
                    aabbRenderersPool.Del(entity);
                }
                
                foreach (var entity in triggersFilter)
                {
                    world.DelEntity(entity);
                }

                foreach (var entity in aabbsFilter)
                {
                    world.DelEntity(entity);
                }

                foreach (var entity in circleCollidersFilter)
                {
                    world.DelEntity(entity);
                }
                
                foreach (var entity in pipeGroupsFilter)
                {
                    world.DelEntity(entity);
                }

                var birdEntity = world.NewEntity();
                var obj = Object.Instantiate(staticData.BirdPrefab, Vector3.zero, Quaternion.identity);
                ref var collider = ref colliderPool.Add(birdEntity);
                ref var transform = ref transformPool.Add(birdEntity);
                ref var bird = ref birdsPool.Add(birdEntity);
                ref var rigidbody = ref rigidbodiesPool.Add(birdEntity);
                controlledByPlayer.Add(birdEntity);

                rigidbody.AffectedByPhysics = false;
                rigidbody.HasCustomGravity = true;
                rigidbody.CustomGravity = new Vector2(0, -shared.StaticData.Difficulty.BirdFallSpeed);

                bird.Renderer = obj;
                collider.Radius = shared.StaticData.BirdColliderRadius;
                transform.Position = new Vector2(-2f, 0);

                
                ui.LoseScreen.Active = false;
                ui.InGame.Active = false;
                ui.MainMenu.Active = true;
                
                ui.InGame.Score.SetString(0.ToString());
                commandsPool.Del(command);
            }
        }
    }
}