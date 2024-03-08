using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class PlaygroundInitSystem : IEcsInitSystem
    {
        private EcsWorldInject _world = default;
        private EcsPoolInject <TileComp> _timerPool;

        private TileMB _tilePref;
        private float _positionY;
        private List<TileMB> _tilesMB = new List<TileMB>();

        public void Init(IEcsSystems systems)
        {
            PlaygroundMB playground = GameObject.FindObjectOfType<PlaygroundMB>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, _positionY, j);
                    tempTile.name = $"{_tilePref.name} [{i}]:[{j}]";
                    _tilesMB.Add(tempTile);

                    int entity = _world.Value.NewEntity();
                    ref TileComp tileComp = ref _timerPool.Value.Add(entity);
                   tileComp.MB = tempTile;
                }
            }
        }

        public PlaygroundInitSystem(TileMB pref, float positionY)
        {
            _tilePref = pref;
            _positionY = positionY;
        }
    }
}