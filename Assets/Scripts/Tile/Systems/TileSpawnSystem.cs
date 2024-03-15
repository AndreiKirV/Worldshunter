using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TileSpawnSystem : IEcsRunSystem
    {
        private EcsCustomInject<Data> _data = default;

        private EcsWorldInject _world = default;

        private EcsFilterInject<Inc<SpawnByTileEvent, TileComp>> _spawnByTileFilter = default;

        private EcsPoolInject<TileComp> _tilePool;
        private EcsPoolInject<MustFallComp> _mustFallPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entitySpawnByTile in _spawnByTileFilter.Value)
            {
                TileComp tileComp = _tilePool.Value.Get(entitySpawnByTile);

                SetNewTiles(tileComp.MB);

                _spawnByTileFilter.Pools.Inc1.Del(entitySpawnByTile);
                Debug.Log($"Нажали на тайл {tileComp.MB.Pos}");
            }
        }

        public void SetNewTiles(TileMB targetTile)
        {
            if (targetTile.Pos.x == _data.Value.Map.MaxPosition.x)
            {
                for (int i = (int)_data.Value.Map.MinPosition.y; i <= _data.Value.Map.MaxPosition.y; i++)
                {
                    Debug.Log($"поле есть {_data.Value.Map.Playground.name}");
                    TileMB tempTile = Object.Instantiate<TileMB>(_data.Value.Map.TilePref, _data.Value.Map.Playground.transform);
                    tempTile.transform.localPosition = new Vector3(_data.Value.Map.MaxPosition.x + 1, _data.Value.Map.TilePosY, i);
                    tempTile.name = $"{_data.Value.Map.TilePref.name} [{_data.Value.Map.MaxPosition.x + 1}]:[{i}]";
                    SetDeltaPos(tempTile.Pos);
                    _data.Value.Map.Tiles.Add(tempTile);

                    tempTile.SetWorld(_world.Value);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _tilePool.Value.Add(entity);
                    tileComp.MB = tempTile;
                    tempTile.Entity = entity;

                    ref MustFallComp mustFallComp = ref _mustFallPool.Value.Add(entity);
                    mustFallComp.Transform = tileComp.MB.Transform;
                }

                _data.Value.Map.MaxPosition = new Vector2(_data.Value.Map.MaxPosition.x + 1, _data.Value.Map.MaxPosition.y);
            }
            else if (targetTile.Pos.x == _data.Value.Map.MinPosition.x)
            {
                for (int i = (int)_data.Value.Map.MinPosition.y; i <= _data.Value.Map.MaxPosition.y; i++)
                {
                    Debug.Log($"поле есть {_data.Value.Map.Playground.name}");
                    TileMB tempTile = Object.Instantiate<TileMB>(_data.Value.Map.TilePref, _data.Value.Map.Playground.transform);
                    tempTile.transform.localPosition = new Vector3(_data.Value.Map.MinPosition.x - 1, _data.Value.Map.TilePosY, i);
                    tempTile.name = $"{_data.Value.Map.TilePref.name} [{_data.Value.Map.MinPosition.x - 1}]:[{i}]";
                    SetDeltaPos(tempTile.Pos);
                    _data.Value.Map.Tiles.Add(tempTile);

                    tempTile.SetWorld(_world.Value);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _tilePool.Value.Add(entity);
                    tileComp.MB = tempTile;
                    tempTile.Entity = entity;

                    ref MustFallComp mustFallComp = ref _mustFallPool.Value.Add(entity);
                    mustFallComp.Transform = tileComp.MB.Transform;
                }

                _data.Value.Map.MinPosition = new Vector2(_data.Value.Map.MinPosition.x - 1, _data.Value.Map.MinPosition.y);
            }

            if (targetTile.Pos.y == _data.Value.Map.MaxPosition.y)
            {
                for (int i = (int)_data.Value.Map.MinPosition.x; i <= _data.Value.Map.MaxPosition.x; i++)
                {
                    Debug.Log($"поле есть {_data.Value.Map.Playground.name}");
                    TileMB tempTile = Object.Instantiate<TileMB>(_data.Value.Map.TilePref, _data.Value.Map.Playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, _data.Value.Map.TilePosY, _data.Value.Map.MaxPosition.y + 1);
                    tempTile.name = $"{_data.Value.Map.TilePref.name} [{i}]:[{_data.Value.Map.MaxPosition.y + 1}]";
                    SetDeltaPos(tempTile.Pos);
                    _data.Value.Map.Tiles.Add(tempTile);

                    tempTile.SetWorld(_world.Value);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _tilePool.Value.Add(entity);
                    tileComp.MB = tempTile;
                    tempTile.Entity = entity;

                    ref MustFallComp mustFallComp = ref _mustFallPool.Value.Add(entity);
                    mustFallComp.Transform = tileComp.MB.Transform;
                }

                _data.Value.Map.MaxPosition = new Vector2(_data.Value.Map.MaxPosition.x, _data.Value.Map.MaxPosition.y + 1);
            }
            else if (targetTile.Pos.y == _data.Value.Map.MinPosition.y)
            {
                for (int i = (int)_data.Value.Map.MinPosition.x; i <= _data.Value.Map.MaxPosition.x; i++)
                {
                    Debug.Log($"поле есть {_data.Value.Map.Playground.name}");
                    TileMB tempTile = Object.Instantiate<TileMB>(_data.Value.Map.TilePref, _data.Value.Map.Playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, _data.Value.Map.TilePosY, _data.Value.Map.MinPosition.y - 1);
                    tempTile.name = $"{_data.Value.Map.TilePref.name} [{i}]:[{_data.Value.Map.MinPosition.y - 1}]";
                    SetDeltaPos(tempTile.Pos);
                    _data.Value.Map.Tiles.Add(tempTile);

                    tempTile.SetWorld(_world.Value);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _tilePool.Value.Add(entity);
                    tileComp.MB = tempTile;
                    tempTile.Entity = entity;

                    ref MustFallComp mustFallComp = ref _mustFallPool.Value.Add(entity);
                    mustFallComp.Transform = tileComp.MB.Transform;
                }

                _data.Value.Map.MinPosition = new Vector2(_data.Value.Map.MinPosition.x, _data.Value.Map.MinPosition.y - 1);
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