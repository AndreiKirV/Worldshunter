using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _systems;

        [SerializeField] private Data _data;
        [SerializeField] private List<CardSO> _cardsSO;

        //private Data _data;

        void Start()
        {

            _world = new EcsWorld();
            PreStart();

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
                .Add(new CardsViewSystem())
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
            //определил дальность появления тайлов и геймштук в мире
            _data.Map.TileStartPosY = MathF.Abs(_data.Canvas.transform.position.z - _data.Map.Playground.transform.position.z) - 1;
            _data.Map.GameChipStartPosY = MathF.Abs(_data.Canvas.transform.position.z - _data.Map.Playground.transform.position.z) - 1;
            //определил дальность появления тайлов и геймштук в мире

            //инициализирую ui элементы, чтобы не тыкалось мимо ui
            UIEventer[] eventers = FindObjectsOfType<UIEventer>();
            foreach (var item in eventers)
            {
                item.SetData();
            }
            //инициализирую ui элементы, чтобы не тыкалось мимо ui

            //создаю стартовые карты
            for (int i = 0; i < 12; i++)
            {
                CardMB cardMB = Instantiate(_data.CardMBPref, _data.CardsContentTransform);
                cardMB.TemporaryParentTransform = _data.CardTemporaryParentTransform;
                cardMB.SetWorld(_world);
            }
            //создаю стартовые карты
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