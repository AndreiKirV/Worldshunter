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
            _playground = GameObject.FindObjectOfType<PlaygroundMB>();
            _data.Value.Map.Playground = _playground;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    TileMB tempTile = Object.Instantiate<TileMB>(_data.Value.Map.TilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, _data.Value.Map.TilePosY, j);
                    tempTile.name = $"{_data.Value.Map.TilePref.name} [{i}]:[{j}]";
                    _data.Value.Map.Tiles.Add(tempTile);
                    SetDeltaPos(tempTile.Pos);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _tilePool.Value.Add(entity);
                    tileComp.MB = tempTile;
                    tempTile.Entity = entity;

                    ref MustFallComp mustFallComp = ref _mustFallPool.Value.Add(entity);
                    mustFallComp.Transform = tileComp.MB.Transform;
                }
            }

            foreach (var item in _data.Value.Map.Tiles)
            {
                item.SetWorld(_world.Value);
            }
        }

        private void SetDeltaPos(Vector2 target)
        {
            if (target.x > _data.Value.Map.MaxPosition.x && target.y > _data.Value.Map.MaxPosition.y)
                _data.Value.Map.MaxPosition = target;
            else if (target.x < _data.Value.Map.MinPosition.x && target.y < _data.Value.Map.MinPosition.y)
                _data.Value.Map.MinPosition = target;
        }
    }
}