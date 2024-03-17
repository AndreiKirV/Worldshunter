using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        IEcsSystems _systems;

        //внешка
        [SerializeField] private Map _map;
        [SerializeField] private Gravity _gravity;
        [SerializeField] private GameChipMB _gameChipPref;

        private Data _data;

        void Start()
        {
            _world = new EcsWorld();

            _data = new Data();
            _data.Map = _map;
            _data.Gravity = _gravity;
            _data.GameChipPref = _gameChipPref;

            _systems = new EcsSystems(_world);
            _systems
                //TODO Timer
                .Add(new TimerInitSystem())
                .Add(new TimerSystem())
                .Add(new TickSystem())//TODO для примера, убрать
                                      //TODO Timer

                //TODO Tile
                .Add(new PlaygroundInitSystem())
                .Add(new TileGravitySystems())
                .Add(new TileSpawnSystem())
                //TODO Tile

                //TODO GameChip
                .Add(new GameChipSpawnSystem())
                .Add(new GameChipGravitySystems())
                //TODO GameChip

                //TODO events
                .Add(new EventSystem())
                //TODO events


                ;
            // register additional worlds here, for example:
            // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
            // add debug systems for custom worlds here, for example:
            // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
            _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif

            _systems.Inject(_data);

            _systems.Init();
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