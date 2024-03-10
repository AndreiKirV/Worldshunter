using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TileGravitySystems : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TileComp>> _tileFilter = default;
        private EcsPoolInject<TileComp> _tilePool;
        private float _speed = 5;
        private TileMB _curTile;

        public void Run(IEcsSystems systems)
        {
            foreach (var entityTile in _tileFilter.Value)
            {
                TileComp tempTileComp = _tilePool.Value.Get(entityTile);
                Saves.Data.GetInstance().Tiles.Add(tempTileComp.MB);

                if (_curTile == null)
                    _curTile = tempTileComp.MB;

                if (_curTile.Transform.localPosition.y > 0)
                    _curTile.Transform.localPosition = new Vector3(_curTile.Transform.localPosition.x, _curTile.Transform.localPosition.y - Time.deltaTime * _speed, _curTile.GameObject.transform.localPosition.z);
                else
                {
                    _curTile.Mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    _curTile.Transform.localPosition = new Vector3(_curTile.Transform.localPosition.x, 0, _curTile.GameObject.transform.localPosition.z);
                    _curTile = null;
                }
            }
        }
    }
}