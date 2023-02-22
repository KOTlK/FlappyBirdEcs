using Game.Runtime.Systems.Events.Bird;
using Game.Runtime.Systems.Events.Bird.Movement;
using Game.Runtime.Components.Pipes.Events.Input;
using Game.Runtime.Services.Rendering;
using Game.Runtime.Services.Rendering.Background;
using Game.Runtime.Services.Rendering.Pipes;
using Game.Runtime.Services.Score;
using Game.Runtime.Services.UI;
using Game.Runtime.Systems.Events.PlayerInput;
using Game.Runtime.Systems.Events;
using Game.Runtime.Systems.Physics;
using Game.Runtime.Systems.Pipes;
using Game.Runtime.Systems.Rendering;
using Game.Runtime.Systems.Score;
using Game.Runtime.Systems.Session;
using Game.Runtime.Systems.Time;
using Leopotam.EcsLite;

#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif
using UnityEngine;
using Time = Game.Runtime.Services.Time;

namespace Game.Runtime.Startup
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private StaticData _staticData;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private PipesFactory _pipesFactory;
        [SerializeField] private DebugRoot _debugRoot;
        [SerializeField] private UiRoot _uiRoot;
        [SerializeField] private ParallaxBackground _background;
        
        private EcsWorld _world;
        private IEcsSystems _update;
        private IEcsSystems _fixedUpdate;

        private void Awake()
        {
            var sharedData = new Shared
            {
                StaticData = _staticData,
                Time = new Time(),
                RenderRoot = new RenderRoot()
                {
                    PipesFactory = _pipesFactory
                },
                Screen = new Services.Rendering.Screen(_mainCamera),
                DebugAABB = _debugRoot.AABB,
                CircleDebugFactory = _debugRoot.CircleColliderFactory,
                UserInterface = _uiRoot,
                Score = new GameScore(),
                Background = _background,
            };
            
            _world = new EcsWorld();
            _update = new EcsSystems(_world, sharedData);
            _fixedUpdate = new EcsSystems(_world, sharedData);
            
            _update
               .Add(new SyncTimeSystem())
               .Add(new InitializeSessionSystem())
               .Add(new GenerateBackgroundSystem())
               .Add(new InputSystem())
               .Add(new StartGameSystem())
               .Add(new SpawnCommandsSystem())
               .Add(new PipesPoolingSystem())
               .Add(new SpawnPooledPipesSystem())
               .Add(new PipesSpawnSystem())
               .Add(new PipesMoveSystem())
               .Add(new BirdMovementSystem())
               .Add(new BirdRotationSystem())
               .Add(new ClampBirdPositionSystem())
               .Add(new AABBCircleCollisionDetectionSystem())
               .Add(new ScoreSystem())
               .Add(new BirdDeathSystem())
               .Add(new RestartSystem())
               .Add(new BirdRenderSystem())
               .Add(new BirdAnimationSystem())
               .Add(new PipesRenderSystem())
#if UNITY_EDITOR
               .Add(new EcsWorldDebugSystem())
#endif
               .Add(new ClearEventsSystem<ScreenTapEvent>())
               .Init();

            _fixedUpdate
               .Add(new ApplyGravitySystem())
               .Add(new ApplyPhysicsSystem())
               .Init();
        }

        private void Update()
        {
            _update.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdate.Run();
        }

        private void OnDestroy()
        {
            _update.Destroy();
            _fixedUpdate.Destroy();
            _world.Destroy();
        }
    }
}