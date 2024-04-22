using System;
using System.Collections;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        IEcsSystems _systems;

        [SerializeField] private Data _data;

        //private Data _data;

        void Start()
        {
            PreStart();

            _world = new EcsWorld();

            _systems = new EcsSystems(_world);
            _systems
                //TODO Timer
                .Add(new TimerInitSystem())
                .Add(new TimerSystem())
                .Add(new TickSystem())//TODO для примера, убрать

                //TODO Tile
                .Add(new PlaygroundInitSystem())
                .Add(new TileSpawnSystem())
                .Add(new TileGravitySystems())
                //TODO Tile

                //TODO GameChip
                .Add(new GameChipSpawnSystem())
                .Add(new GameChipGravitySystems())
                //TODO GameChip

                //TODO events
                .Add(new EventSystem())
                //TODO events


                //TODO input
                //0.Add(new InputSystem(_camera));
                //TODO input

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

            //PostStart();
            //StartCoroutine(DelayCoroutine(PostStart, () => _data.Gravity.Scale = 300, 60, 0.1f));
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

        private void PreStart()
        {
            _data.Map.TileStartPosY = MathF.Abs(_data.Canvas.transform.position.z - _data.Map.Playground.transform.position.z) - 1;
            _data.Map.GameChipStartPosY = MathF.Abs(_data.Canvas.transform.position.z - _data.Map.Playground.transform.position.z) - 1;
        }

        private void PostStart()
        {
            _data.Gravity.Scale = 300;
            _data.Gravity.IsOneByOne = false;
            _data.Map.Tiles.Find(item => item.Pos == _data.Map.MinPosition).ForceClick();
        }

        IEnumerator DelayCoroutine(Action action, Action endAction, int value, float delay)
        {
            for (int i = 0; i < value; i++)
            {
                yield return new WaitForSeconds(delay);
                action?.Invoke();
            }

            endAction?.Invoke();
        }
    }
}