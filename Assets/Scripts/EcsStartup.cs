using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Saves;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePref;
        [SerializeField] private float _tilePosY;
        [SerializeField] private Data _data;
        EcsWorld _world;
        IEcsSystems _systems;
        
        //_globalInitSystems.Inject(_state);

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                //TODO Timer
                .Add(new TimerInitSystem())
                .Add(new TimerSystem())
                .Add(new TickSystem())//TODO для примера, убрать
                //TODO Timer

                //TODO Tile
                .Add(new PlaygroundInitSystem(_tilePref.GetComponent<TileMB>(), _tilePosY))
                .Add(new TileGravitySystems())
                .Add(new TileSpawnSystem())
                //TODO Tile

                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject()
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}