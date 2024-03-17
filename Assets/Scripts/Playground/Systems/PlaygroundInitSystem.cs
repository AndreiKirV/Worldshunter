using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class PlaygroundInitSystem : IEcsInitSystem
    {
        private EcsCustomInject<Data> _data = default;

        private EcsWorldInject _world = default;

        private EcsPoolInject<TileComp> _tilePool;
        private EcsPoolInject<MustFallComp> _mustFallPool;

        private PlaygroundMB _playground;

        public void Init(IEcsSystems systems)
        {
            _playground = _data.Value.Map.Playground;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    TileMB tempTile = SpawnSystem.StartSpawn<TileMB>(_playground.transform, _data.Value.Map.TilePref);//Object.Instantiate<TileMB>(_data.Value.Map.TilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, _data.Value.Map.TileStartPosY, j);
                    tempTile.name = $"{_data.Value.Map.TilePref.name} [{i}]:[{j}]";
                    _data.Value.Map.Tiles.Add(tempTile);
                    _data.Value.Map.SetDeltaPos(tempTile.Pos);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _tilePool.Value.Add(entity);
                    tileComp.MB = tempTile;
                    tempTile.Entity = entity;

                    ref MustFallComp mustFallComp = ref _mustFallPool.Value.Add(entity);
                }
            }

            foreach (var item in _data.Value.Map.Tiles)
            {
                item.SetWorld(_world.Value);
            }
        }
    }
}