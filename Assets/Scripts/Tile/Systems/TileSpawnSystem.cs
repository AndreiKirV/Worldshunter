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
        private EcsFilterInject<Inc<TimerComp>> _timerFilter = default;

        private EcsPoolInject<TileComp> _tilePool;
        private EcsPoolInject<MustFallComp> _mustFallPool;
        private EcsPoolInject<TimerComp> _timerPool;

        private float _timeSpawn = 2;
        private float _currentTime;
        private bool _isOneByOne = true;

        public void Run(IEcsSystems systems)
        {
            _currentTime += Time.deltaTime;/* 

            foreach (var entity in _timerFilter.Value)
            {
                TimerComp timerComp = _timerPool.Value.Get(entity);//TODO дописать по таемеру
            } */

            foreach (var entitySpawnByTile in _spawnByTileFilter.Value)
            {
                TileComp tileComp = _tilePool.Value.Get(entitySpawnByTile);
                SetNewTiles(tileComp.MB);

                Debug.Log($"Нажали на тайл {tileComp.MB.Pos}");
                _spawnByTileFilter.Pools.Inc1.Del(entitySpawnByTile);
            }
        }

        public void SetNewTiles(TileMB targetTile)
        {
            if (targetTile.Pos.x == _data.Value.Map.MaxPosition.x)
            {
                for (int i = (int)_data.Value.Map.MinPosition.y; i <= _data.Value.Map.MaxPosition.y; i++)
                {
                    TileMB tempTile = SetTile();//TODO вывести в отдельный метод

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
                    TileMB tempTile = SetTile();

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
                    TileMB tempTile = SetTile();

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
                    TileMB tempTile = SetTile();

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

        private TileMB SetTile()
        {
            TileMB tempTile = Object.Instantiate<TileMB>(_data.Value.Map.TilePref, _data.Value.Map.Playground.transform);
            _currentTime = 0;
            return tempTile;
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