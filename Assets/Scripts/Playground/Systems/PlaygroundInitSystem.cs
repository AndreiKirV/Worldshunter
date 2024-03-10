using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class PlaygroundInitSystem : IEcsInitSystem
    {
        private EcsWorldInject _world = default;
        private EcsPoolInject<TileComp> _timerPool;

        private TileMB _tilePref;
        private float _positionY;
        private Vector2 _maxPosition = Vector2.zero;
        private Vector2 _minPosition = Vector2.zero;

        private PlaygroundMB _playground;

        public void Init(IEcsSystems systems)
        {
            _playground = GameObject.FindObjectOfType<PlaygroundMB>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, _positionY, j);
                    tempTile.name = $"{_tilePref.name} [{i}]:[{j}]";
                    tempTile.OnMouseDownAction += (tempTile) => Debug.Log($"{tempTile.name}");
                    tempTile.OnMouseDownAction += SetNewTiles;
                    Saves.Data.GetInstance().Tiles.Add(tempTile);
                    SetDeltaPos(tempTile.Pos);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                    tileComp.MB = tempTile;
                }
            }
        }

        private void SetDeltaPos(Vector2 target)
        {
            if (target.x > _maxPosition.x && target.y > _maxPosition.y)
                _maxPosition = target;
            else if (target.x < _minPosition.x && target.y < _minPosition.y)
                _minPosition = target;
        }

        public void SetNewTiles(TileMB targetTile)
        {
            Debug.Log("Sravnivaiu");
            if (targetTile.Pos.x == _maxPosition.x)
            {
                Debug.Log("Zashel");
                for (int i = (int)_minPosition.y; i <= _maxPosition.y; i++)
                {
                    Debug.Log(_playground);
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(_maxPosition.x + 1, _positionY, i);
                    tempTile.name = $"{_tilePref.name} [{_maxPosition.x + 1}]:[{i}]";
                    tempTile.OnMouseDownAction += (tempTile) => Debug.Log($"{tempTile.name} {_maxPosition.x + 1}");
                    SetDeltaPos(tempTile.Pos);
                    Saves.Data.GetInstance().Tiles.Add(tempTile);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                    tileComp.MB = tempTile;

                    tempTile.OnMouseDownAction += (tempTile) => SetNewTiles(tempTile);
                }

                _maxPosition = new Vector2(_maxPosition.x + 1, _maxPosition.y);
            }
            else if (targetTile.Pos.x == _minPosition.x)
            {
                Debug.Log("Zashel");
                for (int i = (int)_minPosition.y; i <= _maxPosition.y; i++)
                {
                    Debug.Log(_playground);
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(_minPosition.x - 1, _positionY, i);
                    tempTile.name = $"{_tilePref.name} [{_minPosition.x - 1}]:[{i}]";
                    tempTile.OnMouseDownAction += (tempTile) => Debug.Log($"{tempTile.name} {_minPosition.x - 1}");
                    SetDeltaPos(tempTile.Pos);
                    Saves.Data.GetInstance().Tiles.Add(tempTile);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                    tileComp.MB = tempTile;

                    tempTile.OnMouseDownAction += (tempTile) => SetNewTiles(tempTile);
                }

                _minPosition = new Vector2(_minPosition.x - 1, _minPosition.y);
            }

            if (targetTile.Pos.y == _maxPosition.y)
            {
                Debug.Log("Zashel");
                for (int i = (int)_minPosition.x; i <= _maxPosition.x; i++)
                {
                    Debug.Log(_playground);
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, _positionY, _maxPosition.y + 1);
                    tempTile.name = $"{_tilePref.name} [{i}]:[{_maxPosition.y + 1}]";
                    tempTile.OnMouseDownAction += (tempTile) => Debug.Log($"{tempTile.name} {_maxPosition.y + 1}");
                    SetDeltaPos(tempTile.Pos);
                    Saves.Data.GetInstance().Tiles.Add(tempTile);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                    tileComp.MB = tempTile;

                    tempTile.OnMouseDownAction += (tempTile) => SetNewTiles(tempTile);
                }

                _maxPosition = new Vector2(_maxPosition.x, _maxPosition.y + 1);
            }
            else if (targetTile.Pos.y == _minPosition.y)
            {
                Debug.Log("Zashel");
                for (int i = (int)_minPosition.x; i <= _maxPosition.x; i++)
                {
                    Debug.Log(_playground);
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, _playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, _positionY, _minPosition.y - 1);
                    tempTile.name = $"{_tilePref.name} [{i}]:[{_minPosition.y - 1}]";
                    tempTile.OnMouseDownAction += (tempTile) => Debug.Log($"{tempTile.name} {_minPosition.y - 1}");
                    SetDeltaPos(tempTile.Pos);
                    Saves.Data.GetInstance().Tiles.Add(tempTile);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                    tileComp.MB = tempTile;

                    tempTile.OnMouseDownAction += (tempTile) => SetNewTiles(tempTile);
                }

                _minPosition = new Vector2(_minPosition.x, _minPosition.y - 1);
            }
        }

        public PlaygroundInitSystem(TileMB pref, float positionY)
        {
            _tilePref = pref;
            _positionY = positionY;
        }
    }
}