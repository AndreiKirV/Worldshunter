using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TileSpawnSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsPoolInject<TileComp> _timerPool;
        private EcsFilterInject<Inc<MapComp>> _mapFilter = default;
        private EcsPoolInject<MapComp> _mapPool;

        private TileMB _tilePref;
        private float _positionY;
        private Vector2 _maxPosition = Vector2.zero;
        private Vector2 _minPosition = Vector2.zero;
        private PlaygroundMB _playground;

        public void Run(IEcsSystems systems)
        {
            foreach (var entityMap in _mapFilter.Value)
            {
                MapComp mapComp = _mapPool.Value.Get(entityMap);

                _maxPosition = mapComp.MaxPosition;
                _minPosition = mapComp.MinPosition;

                _mapPool.Value.Del(entityMap);
                Debug.Log($"{_minPosition} {_maxPosition}");
                Debug.Log(Saves.Data.GetInstance().Tiles.Count);
            }
        }

        public void SetNewTiles(TileMB targetTile)
        {
            Debug.Log("Sravnivaiu");

            if (targetTile.Pos.x == _maxPosition.x)
            {
                for (int i = (int)_minPosition.y; i <= _maxPosition.y; i++)
                {
                    Debug.Log(_playground);
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(_maxPosition.x + 1, _positionY, i);
                    tempTile.name = $"{_tilePref.name} [{_maxPosition.x + 1}]:[{i}]";
                    SetDeltaPos(tempTile.Pos);
                    Saves.Data.GetInstance().Tiles.Add(tempTile);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                    tileComp.MB = tempTile;
                }

                _maxPosition = new Vector2(_maxPosition.x + 1, _maxPosition.y);
            }
            else if (targetTile.Pos.x == _minPosition.x)
            {
                for (int i = (int)_minPosition.y; i <= _maxPosition.y; i++)
                {
                    Debug.Log(_playground);
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(_minPosition.x - 1, _positionY, i);
                    tempTile.name = $"{_tilePref.name} [{_minPosition.x - 1}]:[{i}]";
                    SetDeltaPos(tempTile.Pos);
                    Saves.Data.GetInstance().Tiles.Add(tempTile);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                    tileComp.MB = tempTile;
                }

                _minPosition = new Vector2(_minPosition.x - 1, _minPosition.y);
            }

            if (targetTile.Pos.y == _maxPosition.y)
            {
                for (int i = (int)_minPosition.x; i <= _maxPosition.x; i++)
                {
                    Debug.Log(_playground);
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, _positionY, _maxPosition.y + 1);
                    tempTile.name = $"{_tilePref.name} [{i}]:[{_maxPosition.y + 1}]";
                    SetDeltaPos(tempTile.Pos);
                    Saves.Data.GetInstance().Tiles.Add(tempTile);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                    tileComp.MB = tempTile;
                }

                _maxPosition = new Vector2(_maxPosition.x, _maxPosition.y + 1);
            }
            else if (targetTile.Pos.y == _minPosition.y)
            {
                for (int i = (int)_minPosition.x; i <= _maxPosition.x; i++)
                {
                    Debug.Log(_playground);
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, _positionY, _minPosition.y - 1);
                    tempTile.name = $"{_tilePref.name} [{i}]:[{_minPosition.y - 1}]";
                    SetDeltaPos(tempTile.Pos);
                    Saves.Data.GetInstance().Tiles.Add(tempTile);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                    tileComp.MB = tempTile;
                }

                _minPosition = new Vector2(_minPosition.x, _minPosition.y - 1);
            }
        }

        private void SetDeltaPos(Vector2 target)
        {
            if (target.x > _maxPosition.x && target.y > _maxPosition.y)
                _maxPosition = target;
            else if (target.x < _minPosition.x && target.y < _minPosition.y)
                _minPosition = target;
        }
    }
}