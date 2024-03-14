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
        private EcsPoolInject<MustFallComp> _mustFallPool;
        private EcsPoolInject<MapComp> _mapPool;

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
                    Saves.Data.GetInstance().Tiles.Add(tempTile);
                    SetDeltaPos(tempTile.Pos);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                    tileComp.MB = tempTile;
                    tempTile.Entity = entity;

                    ref MustFallComp mustFallComp = ref _mustFallPool.Value.Add(entity);
                    mustFallComp.Transform = tileComp.MB.Transform;
                }
            }

            int entityMap = _world.Value.NewEntity();
            ref MapComp mapComp = ref _mapPool.Value.Add(entityMap);
            mapComp.MaxPosition = _maxPosition;
            mapComp.MinPosition = _minPosition;
        }

        public PlaygroundInitSystem(TileMB pref, float positionY)
        {
            _tilePref = pref;
            _positionY = positionY;
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